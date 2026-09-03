#nullable enable

using System.Text.Json.Serialization;

namespace CommercialManagement.Application.DTOs
{
    /// <summary>
    /// DTO pour afficher une commande
    /// </summary>
    public class CommandeDTO
    {
        public int Identifiant { get; set; }
        public string Numéro_de_commande { get; set; } = string.Empty;
        public int Client_associé { get; set; }
        public string ClientNom { get; set; } = string.Empty;
        public DateTime Date_de_commande { get; set; }
        public string Statut_de_la_commande { get; set; } = string.Empty;
        public decimal Total_HT { get; set; }
        public decimal Total_TTC { get; set; }
        public List<LigneDeCommandeDTO> Lignes_de_commande { get; set; } = new();
    }

    /// <summary>
    /// DTO pour afficher une ligne de commande
    /// </summary>
    public class LigneDeCommandeDTO
    {
        public int Identifiant { get; set; }
        [JsonPropertyName("produit_identifiant")]
        public int Produit_Identifiant { get; set; }
        public string ProduitNom { get; set; } = string.Empty;
        public string ProduitReference { get; set; } = string.Empty;
        public int Quantité { get; set; }
        public decimal Prix_unitaire { get; set; }
        public decimal Total_ligne { get; set; }
    }

    /// <summary>
    /// DTO pour créer une commande
    /// </summary>
    public class CreateCommandeDTO
    {
        public int Client_associé { get; set; }
        public string Statut_de_la_commande { get; set; } = "Brouillon";
        public List<CreateLigneDeCommandeDTO> Lignes_de_commande { get; set; } = new();
    }

    /// <summary>
    /// DTO pour créer une ligne de commande
    /// </summary>
    public class CreateLigneDeCommandeDTO
    {
        [JsonPropertyName("produit_identifiant")]
        public int Produit_Identifiant { get; set; }
        public int Quantité { get; set; }
    }

    /// <summary>
    /// DTO pour mettre à jour une commande
    /// </summary>
    public class UpdateCommandeDTO
    {
        public int Client_associé { get; set; }
        public string Statut_de_la_commande { get; set; } = string.Empty;
        public List<UpdateLigneDeCommandeDTO> Lignes_de_commande { get; set; } = new();
    }

    /// <summary>
    /// DTO pour mettre à jour une ligne de commande
    /// </summary>
    public class UpdateLigneDeCommandeDTO
    {
        public int? Identifiant { get; set; }
        [JsonPropertyName("produit_identifiant")]
        public int Produit_Identifiant { get; set; }
        public int Quantité { get; set; }
    }
}