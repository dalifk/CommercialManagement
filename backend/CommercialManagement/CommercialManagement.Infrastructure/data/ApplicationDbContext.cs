using Microsoft.EntityFrameworkCore;
using CommercialManagement.Domain.Entities;

namespace CommercialManagement.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<Ligne_de_commande> Lignes_de_commande { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ Client configuration with PRIMARY KEY
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Identifiant);  // ← THIS IS THE FIX!
                entity.Property(e => e.Nom).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Prénom_ou_raison_sociale).HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Téléphone).HasMaxLength(20);
                entity.Property(e => e.Adresse).HasMaxLength(500);
                entity.Property(e => e.Date_de_création).HasDefaultValueSql("CURRENT_TIMESTAMP");
                
                entity.HasMany(e => e.Commandes)
                      .WithOne(e => e.Client)
                      .HasForeignKey(e => e.Client_associé)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Produit configuration
            modelBuilder.Entity<Produit>(entity =>
            {
                entity.HasKey(e => e.Identifiant);
                entity.Property(e => e.Référence).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Référence).IsUnique();
                entity.Property(e => e.Nom_du_produit).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Prix_unitaire_HT).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Quantité_en_stock).HasDefaultValue(0);
                entity.Property(e => e.Date_de_création).HasDefaultValueSql("CURRENT_TIMESTAMP");
                
                entity.HasMany(e => e.Lignes_de_commande)
                      .WithOne(e => e.Produit)
                      .HasForeignKey(e => e.Produit_Identifiant)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Commande configuration
            modelBuilder.Entity<Commande>(entity =>
            {
                entity.HasKey(e => e.Identifiant);
                entity.Property(e => e.Numéro_de_commande).IsRequired().HasMaxLength(20);
                entity.HasIndex(e => e.Numéro_de_commande).IsUnique();
                entity.Property(e => e.Statut_de_la_commande).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Total_HT).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Total_TTC).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Date_de_commande).HasDefaultValueSql("CURRENT_TIMESTAMP");
                
                entity.HasOne(e => e.Client)
                      .WithMany(e => e.Commandes)
                      .HasForeignKey(e => e.Client_associé)
                      .OnDelete(DeleteBehavior.Restrict);
                
                entity.HasMany(e => e.Lignes_de_commande)
                      .WithOne(e => e.Commande)
                      .HasForeignKey(e => e.Commande_Identifiant)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Ligne_de_commande configuration
            modelBuilder.Entity<Ligne_de_commande>(entity =>
            {
                entity.HasKey(e => e.Identifiant);
                entity.Property(e => e.Quantité).IsRequired();
                entity.Property(e => e.Prix_unitaire).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Total_ligne).HasColumnType("decimal(18,2)");
                
                entity.HasOne(e => e.Commande)
                      .WithMany(e => e.Lignes_de_commande)
                      .HasForeignKey(e => e.Commande_Identifiant)
                      .OnDelete(DeleteBehavior.Cascade);
                
                entity.HasOne(e => e.Produit)
                      .WithMany(e => e.Lignes_de_commande)
                      .HasForeignKey(e => e.Produit_Identifiant)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ✅ Utilisateur configuration
           // Configuration de l'entité Utilisateur
            modelBuilder.Entity<Utilisateur>(entity =>
                    {
                        entity.HasKey(e => e.Identifiant);
                        entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                        entity.HasIndex(e => e.Email).IsUnique();
                        entity.Property(e => e.MotDePasse).IsRequired().HasMaxLength(255);
                        entity.Property(e => e.Nom).IsRequired().HasMaxLength(100);
                        entity.Property(e => e.Role).IsRequired().HasMaxLength(50);
                        entity.Property(e => e.DateCreation).HasDefaultValueSql("CURRENT_TIMESTAMP");
                    });
        }
    }
}