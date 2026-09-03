using System;

namespace CommercialManagement.Domain.Entities
{
    public class Utilisateur
    {
        public int Identifiant { get; set; }
        public string Email { get; set; } = string.Empty;
        public string MotDePasse { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime DateCreation { get; set; }

        public Utilisateur()
        {
            DateCreation = DateTime.UtcNow;
        }
    }
}