using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommercialManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUtilisateursTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Utilisateurs",
                keyColumn: "Identifiant",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Utilisateurs",
                columns: new[] { "Identifiant", "DateCreation", "Email", "MotDePasse", "Nom", "Role" },
                values: new object[] { 1, new DateTime(2026, 9, 3, 10, 40, 50, 794, DateTimeKind.Utc).AddTicks(5595), "admin@commercial.com", "Admin123!", "Administrateur", "Admin" });
        }
    }
}
