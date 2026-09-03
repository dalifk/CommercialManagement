using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using CommercialManagement.Infrastructure;
using CommercialManagement.Application.Services;
using CommercialManagement.Application.Services.Interfaces;
using CommercialManagement.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// ============================================
// CONFIGURATION DE L'AUTHENTIFICATION JWT
// ============================================

// Récupération de la clé JWT depuis appsettings.json
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new Exception("JWT Key not configured");
var key = Encoding.UTF8.GetBytes(jwtKey);

// Configuration de l'authentification JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ClockSkew = TimeSpan.Zero // Supprime le délai de validation
    };
});

// Ajout de l'autorisation
builder.Services.AddAuthorization();

// ============================================
// CONFIGURATION DES SERVICES
// ============================================

// Ajout de l'infrastructure (contexte de base de données)
builder.Services.AddInfrastructure(builder.Configuration);

// Ajout des services de l'application
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IProduitService, ProduitService>();
builder.Services.AddScoped<ICommandeService, CommandeService>();

// Ajout des contrôleurs API
builder.Services.AddControllers();

// Configuration de Swagger/OpenAPI avec support JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Gestion Commerciale",
        Version = "v1",
        Description = "API pour la gestion des clients, produits et commandes"
    });

    // Ajout du bouton "Authorize" dans Swagger pour JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n" +
                      "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                      "Example: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

// Configuration CORS pour Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policyBuilder =>
        {
            policyBuilder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

// ============================================
// CONSTRUCTION DE L'APPLICATION
// ============================================

var app = builder.Build();

// ============================================
// CONFIGURATION DU PIPELINE HTTP
// ============================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");

// ✅ IMPORTANT: L'ordre est important ! Authentication puis Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ============================================
// DÉMARRAGE DE L'APPLICATION
// ============================================

app.Run();