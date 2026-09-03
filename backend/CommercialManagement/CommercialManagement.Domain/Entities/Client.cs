using System;
using System.Collections.Generic;

namespace CommercialManagement.Domain.Entities
{
    public class Client
    {
        public int Identifiant { get; set; }  // ← This is the primary key
        public string Nom { get; set; } = string.Empty;
        public string? Prénom_ou_raison_sociale { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Téléphone { get; set; }
        public string? Adresse { get; set; }
        public DateTime Date_de_création { get; set; }

        public ICollection<Commande> Commandes { get; set; } = new List<Commande>();

        public Client()
        {
            Date_de_création = DateTime.UtcNow;
        }
    }
}