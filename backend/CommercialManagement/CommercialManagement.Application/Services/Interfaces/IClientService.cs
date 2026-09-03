using System.Collections.Generic;
using System.Threading.Tasks;
using CommercialManagement.Application.DTOs;

namespace CommercialManagement.Application.Services.Interfaces
{
    /// <summary>
    /// Interface du service de gestion des clients
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Récupère tous les clients
        /// </summary>
        Task<IEnumerable<ClientDTO>> GetAllClientsAsync();

        /// <summary>
        /// Récupère un client par son identifiant
        /// </summary>
        Task<ClientDTO> GetClientByIdAsync(int id);

        /// <summary>
        /// Crée un nouveau client
        /// </summary>
        Task<ClientDTO> CreateClientAsync(CreateClientDTO createClientDto);

        /// <summary>
        /// Met à jour un client
        /// </summary>
        Task<ClientDTO> UpdateClientAsync(int id, UpdateClientDTO updateClientDto);

        /// <summary>
        /// Supprime un client
        /// </summary>
        Task DeleteClientAsync(int id);
    }
}