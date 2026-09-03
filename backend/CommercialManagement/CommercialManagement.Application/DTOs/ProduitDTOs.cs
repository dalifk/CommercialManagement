#nullable enable

using System;

namespace CommercialManagement.Application.DTOs
{
    /// <summary>
    /// DTO pour afficher un produit
    /// </summary>
    public class ProduitDTO
    {
        public int Identifiant { get; set; }
        public string Référence { get; set; } = string.Empty;
        public string Nom_du_produit { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Prix_unitaire_HT { get; set; }
        public int Quantité_en_stock { get; set; }
        public DateTime Date_de_création { get; set; }
    }

    /// <summary>
    /// DTO pour créer un produit
    /// </summary>
    public class CreateProduitDTO
    {
        public string Référence { get; set; } = string.Empty;
        public string Nom_du_produit { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Prix_unitaire_HT { get; set; }
        public int Quantité_en_stock { get; set; }
    }

    /// <summary>
    /// DTO pour mettre à jour un produit
    /// </summary>
    public class UpdateProduitDTO
    {
        public string Référence { get; set; } = string.Empty;
        public string Nom_du_produit { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Prix_unitaire_HT { get; set; }
        public int Quantité_en_stock { get; set; }
    }
}