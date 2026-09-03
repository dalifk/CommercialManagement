#nullable enable

using System;
using System.Collections.Generic;

namespace CommercialManagement.Domain.Entities
{
    public class Commande
    {
        public int Identifiant { get; set; }
        public string Numéro_de_commande { get; set; } = string.Empty;
        public int Client_associé { get; set; }
        public DateTime Date_de_commande { get; set; }
        public string Statut_de_la_commande { get; set; } = "Brouillon";
        public decimal Total_HT { get; set; }
        public decimal Total_TTC { get; set; }

        // Navigation properties
        public virtual Client? Client { get; set; }
        public virtual ICollection<Ligne_de_commande> Lignes_de_commande { get; set; } = new List<Ligne_de_commande>();

        public Commande()
        {
            Date_de_commande = DateTime.UtcNow;
            Statut_de_la_commande = "Brouillon";
        }
    }
}