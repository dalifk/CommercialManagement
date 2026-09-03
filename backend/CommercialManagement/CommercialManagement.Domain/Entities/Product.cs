#nullable enable

using System;
using System.Collections.Generic;

namespace CommercialManagement.Domain.Entities
{
    public class Produit
    {
        public int Identifiant { get; set; }
        public string Référence { get; set; } = string.Empty;
        public string Nom_du_produit { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Prix_unitaire_HT { get; set; }
        public int Quantité_en_stock { get; set; }
        public DateTime Date_de_création { get; set; }

        // Navigation property
        public virtual ICollection<Ligne_de_commande> Lignes_de_commande { get; set; } = new List<Ligne_de_commande>();

        public Produit()
        {
            Date_de_création = DateTime.UtcNow;
        }
    }
}