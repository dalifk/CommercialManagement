using System.Collections.Generic;
using System.Threading.Tasks;
using CommercialManagement.Application.DTOs;

namespace CommercialManagement.Application.Services.Interfaces
{
    /// <summary>
    /// Interface du service de gestion des produits
    /// </summary>
    public interface IProduitService
    {
        /// <summary>
        /// Récupère tous les produits
        /// </summary>
        Task<IEnumerable<ProduitDTO>> GetAllProduitsAsync();

        /// <summary>
        /// Récupère un produit par son identifiant
        /// </summary>
        Task<ProduitDTO> GetProduitByIdAsync(int id);

        /// <summary>
        /// Crée un nouveau produit
        /// </summary>
        Task<ProduitDTO> CreateProduitAsync(CreateProduitDTO createProduitDto);

        /// <summary>
        /// Met à jour un produit
        /// </summary>
        Task<ProduitDTO> UpdateProduitAsync(int id, UpdateProduitDTO updateProduitDto);

        /// <summary>
        /// Supprime un produit
        /// </summary>
        Task DeleteProduitAsync(int id);
    }
}