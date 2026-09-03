using Microsoft.EntityFrameworkCore;
using CommercialManagement.Application.DTOs;
using CommercialManagement.Application.Services.Interfaces;
using CommercialManagement.Domain.Entities;
using CommercialManagement.Infrastructure.Data;

namespace CommercialManagement.Application.Services
{
    /// <summary>
    /// Service de gestion des clients
    /// </summary>
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _context;

        public ClientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClientDTO>> GetAllClientsAsync()
        {
            var clients = await _context.Clients
                .OrderBy(c => c.Nom)
                .ToListAsync();

            return clients.Select(c => new ClientDTO
            {
                Identifiant = c.Identifiant,
                Nom = c.Nom,
                Prénom_ou_raison_sociale = c.Prénom_ou_raison_sociale,
                Email = c.Email,
                Téléphone = c.Téléphone,
                Adresse = c.Adresse,
                Date_de_création = c.Date_de_création
            });
        }

        public async Task<ClientDTO> GetClientByIdAsync(int id)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.Identifiant == id);

            if (client == null)
                throw new Exception($"Client avec l'identifiant {id} non trouvé");

            return new ClientDTO
            {
                Identifiant = client.Identifiant,
                Nom = client.Nom,
                Prénom_ou_raison_sociale = client.Prénom_ou_raison_sociale,
                Email = client.Email,
                Téléphone = client.Téléphone,
                Adresse = client.Adresse,
                Date_de_création = client.Date_de_création
            };
        }

        public async Task<ClientDTO> CreateClientAsync(CreateClientDTO createClientDto)
        {
            // Vérifier si l'email existe déjà
            var existingClient = await _context.Clients
                .FirstOrDefaultAsync(c => c.Email == createClientDto.Email);

            if (existingClient != null)
                throw new Exception($"Un client avec l'email {createClientDto.Email} existe déjà");

            var client = new Client
            {
                Nom = createClientDto.Nom,
                Prénom_ou_raison_sociale = createClientDto.Prénom_ou_raison_sociale,
                Email = createClientDto.Email,
                Téléphone = createClientDto.Téléphone,
                Adresse = createClientDto.Adresse,
                Date_de_création = DateTime.UtcNow
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return new ClientDTO
            {
                Identifiant = client.Identifiant,
                Nom = client.Nom,
                Prénom_ou_raison_sociale = client.Prénom_ou_raison_sociale,
                Email = client.Email,
                Téléphone = client.Téléphone,
                Adresse = client.Adresse,
                Date_de_création = client.Date_de_création
            };
        }

        public async Task<ClientDTO> UpdateClientAsync(int id, UpdateClientDTO updateClientDto)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.Identifiant == id);

            if (client == null)
                throw new Exception($"Client avec l'identifiant {id} non trouvé");

            // Vérifier si l'email est déjà utilisé par un autre client
            var existingClient = await _context.Clients
                .FirstOrDefaultAsync(c => c.Email == updateClientDto.Email && c.Identifiant != id);

            if (existingClient != null)
                throw new Exception($"Un client avec l'email {updateClientDto.Email} existe déjà");

            client.Nom = updateClientDto.Nom;
            client.Prénom_ou_raison_sociale = updateClientDto.Prénom_ou_raison_sociale;
            client.Email = updateClientDto.Email;
            client.Téléphone = updateClientDto.Téléphone;
            client.Adresse = updateClientDto.Adresse;

            await _context.SaveChangesAsync();

            return new ClientDTO
            {
                Identifiant = client.Identifiant,
                Nom = client.Nom,
                Prénom_ou_raison_sociale = client.Prénom_ou_raison_sociale,
                Email = client.Email,
                Téléphone = client.Téléphone,
                Adresse = client.Adresse,
                Date_de_création = client.Date_de_création
            };
        }

        public async Task DeleteClientAsync(int id)
        {
            var client = await _context.Clients
                .Include(c => c.Commandes)
                .FirstOrDefaultAsync(c => c.Identifiant == id);

            if (client == null)
                throw new Exception($"Client avec l'identifiant {id} non trouvé");

            // Vérifier si le client a des commandes
            if (client.Commandes.Any())
                throw new Exception("Impossible de supprimer un client qui a des commandes");

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }
}