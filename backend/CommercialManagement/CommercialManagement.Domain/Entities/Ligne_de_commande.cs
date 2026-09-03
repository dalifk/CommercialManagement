#nullable enable

using System;

namespace CommercialManagement.Domain.Entities
{
    public class Ligne_de_commande
    {
        public int Identifiant { get; set; }
        public int Commande_Identifiant { get; set; }
        public int Produit_Identifiant { get; set; }
        public int Quantité { get; set; }
        public decimal Prix_unitaire { get; set; }
        public decimal Total_ligne { get; set; }

        // Navigation properties
        public virtual Commande? Commande { get; set; }
        public virtual Produit? Produit { get; set; }
    }
}