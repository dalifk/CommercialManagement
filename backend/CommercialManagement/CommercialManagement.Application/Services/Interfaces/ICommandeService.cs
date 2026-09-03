using System.Collections.Generic;
using System.Threading.Tasks;
using CommercialManagement.Application.DTOs;

namespace CommercialManagement.Application.Services.Interfaces
{
    /// <summary>
    /// Interface du service de gestion des commandes
    /// </summary>
    public interface ICommandeService
    {
        /// <summary>
        /// Récupère toutes les commandes
        /// </summary>
        Task<IEnumerable<CommandeDTO>> GetAllCommandesAsync();

        /// <summary>
        /// Récupère une commande par son identifiant
        /// </summary>
        Task<CommandeDTO> GetCommandeByIdAsync(int id);

        /// <summary>
        /// Crée une nouvelle commande
        /// </summary>
        Task<CommandeDTO> CreateCommandeAsync(CreateCommandeDTO createCommandeDto);

        /// <summary>
        /// Met à jour une commande
        /// </summary>
        Task<CommandeDTO> UpdateCommandeAsync(int id, UpdateCommandeDTO updateCommandeDto);

        /// <summary>
        /// Supprime une commande
        /// </summary>
        Task DeleteCommandeAsync(int id);

        /// <summary>
        /// Valide une commande (met à jour le stock)
        /// </summary>
        Task ValidateCommandeAsync(int id);
    }
}