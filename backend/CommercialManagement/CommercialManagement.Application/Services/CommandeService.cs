using Microsoft.EntityFrameworkCore;
using CommercialManagement.Application.DTOs;
using CommercialManagement.Application.Services.Interfaces;
using CommercialManagement.Domain.Entities;
using CommercialManagement.Infrastructure.Data;

namespace CommercialManagement.Application.Services
{
    /// <summary>
    /// Service de gestion des commandes
    /// </summary>
    public class CommandeService : ICommandeService
    {
        private readonly ApplicationDbContext _context;

        public CommandeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CommandeDTO>> GetAllCommandesAsync()
        {
            var commandes = await _context.Commandes
                .Include(c => c.Client)
                .Include(c => c.Lignes_de_commande)
                .ThenInclude(l => l.Produit)
                .OrderByDescending(c => c.Date_de_commande)
                .ToListAsync();

            return commandes.Select(MapToCommandeDTO);
        }

        public async Task<CommandeDTO> GetCommandeByIdAsync(int id)
        {
            var commande = await _context.Commandes
                .Include(c => c.Client)
                .Include(c => c.Lignes_de_commande)
                .ThenInclude(l => l.Produit)
                .FirstOrDefaultAsync(c => c.Identifiant == id);

            if (commande == null)
                throw new Exception($"Commande avec l'identifiant {id} non trouvée");

            return MapToCommandeDTO(commande);
        }

        public async Task<CommandeDTO> CreateCommandeAsync(CreateCommandeDTO createCommandeDto)
        {
            // Vérifier que le client existe
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.Identifiant == createCommandeDto.Client_associé);

            if (client == null)
                throw new Exception($"Client avec l'identifiant {createCommandeDto.Client_associé} non trouvé");

            // Vérifier qu'il y a au moins un produit
            if (createCommandeDto.Lignes_de_commande == null || !createCommandeDto.Lignes_de_commande.Any())
                throw new Exception("La commande doit contenir au moins un produit");

            // Créer la commande
            var commande = new Commande
            {
                Numéro_de_commande = await GenerateOrderNumberAsync(),
                Client_associé = createCommandeDto.Client_associé,
                Date_de_commande = DateTime.UtcNow,
                Statut_de_la_commande = createCommandeDto.Statut_de_la_commande ?? "Brouillon"
            };

            decimal totalHT = 0;

            // Ajouter les lignes de commande
            foreach (var lineDto in createCommandeDto.Lignes_de_commande)
            {
                var produit = await _context.Produits
                    .FirstOrDefaultAsync(p => p.Identifiant == lineDto.Produit_Identifiant);

                if (produit == null)
                    throw new Exception($"Produit avec l'identifiant {lineDto.Produit_Identifiant} non trouvé");

                if (lineDto.Quantité <= 0)
                    throw new Exception("La quantité doit être supérieure à 0");

                // Si la commande est validée, vérifier le stock
                if (commande.Statut_de_la_commande == "Validée" && lineDto.Quantité > produit.Quantité_en_stock)
                    throw new Exception($"Stock insuffisant pour le produit {produit.Nom_du_produit}. Stock disponible: {produit.Quantité_en_stock}");

                var ligne = new Ligne_de_commande
                {
                    Produit_Identifiant = lineDto.Produit_Identifiant,
                    Quantité = lineDto.Quantité,
                    Prix_unitaire = produit.Prix_unitaire_HT,
                    Total_ligne = lineDto.Quantité * produit.Prix_unitaire_HT
                };

                commande.Lignes_de_commande.Add(ligne);
                totalHT += ligne.Total_ligne;

                // Si commande validée, mettre à jour le stock
                if (commande.Statut_de_la_commande == "Validée")
                {
                    produit.Quantité_en_stock -= lineDto.Quantité;
                }
            }

            commande.Total_HT = totalHT;
            commande.Total_TTC = totalHT * 1.19m; // TVA 19%

            _context.Commandes.Add(commande);
            await _context.SaveChangesAsync();

            // Recharger la commande avec toutes les relations
            var createdCommande = await _context.Commandes
                .Include(c => c.Client)
                .Include(c => c.Lignes_de_commande)
                .ThenInclude(l => l.Produit)
                .FirstOrDefaultAsync(c => c.Identifiant == commande.Identifiant);

            return MapToCommandeDTO(createdCommande);
        }

        public async Task<CommandeDTO> UpdateCommandeAsync(int id, UpdateCommandeDTO updateCommandeDto)
        {
            var commande = await _context.Commandes
                .Include(c => c.Client)
                .Include(c => c.Lignes_de_commande)
                .ThenInclude(l => l.Produit)
                .FirstOrDefaultAsync(c => c.Identifiant == id);

            if (commande == null)
                throw new Exception($"Commande avec l'identifiant {id} non trouvée");

            // Vérifier que le client existe
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.Identifiant == updateCommandeDto.Client_associé);

            if (client == null)
                throw new Exception($"Client avec l'identifiant {updateCommandeDto.Client_associé} non trouvé");

            // Ne peut modifier que les commandes en brouillon
            if (commande.Statut_de_la_commande != "Brouillon")
                throw new Exception("Seules les commandes en brouillon peuvent être modifiées");

            commande.Client_associé = updateCommandeDto.Client_associé;

            // Supprimer les anciennes lignes
            _context.Lignes_de_commande.RemoveRange(commande.Lignes_de_commande);
            commande.Lignes_de_commande.Clear();

            decimal totalHT = 0;

            // Ajouter les nouvelles lignes
            foreach (var lineDto in updateCommandeDto.Lignes_de_commande)
            {
                var produit = await _context.Produits
                    .FirstOrDefaultAsync(p => p.Identifiant == lineDto.Produit_Identifiant);

                if (produit == null)
                    throw new Exception($"Produit avec l'identifiant {lineDto.Produit_Identifiant} non trouvé");

                if (lineDto.Quantité <= 0)
                    throw new Exception("La quantité doit être supérieure à 0");

                var ligne = new Ligne_de_commande
                {
                    Produit_Identifiant = lineDto.Produit_Identifiant,
                    Quantité = lineDto.Quantité,
                    Prix_unitaire = produit.Prix_unitaire_HT,
                    Total_ligne = lineDto.Quantité * produit.Prix_unitaire_HT
                };

                commande.Lignes_de_commande.Add(ligne);
                totalHT += ligne.Total_ligne;
            }

            commande.Total_HT = totalHT;
            commande.Total_TTC = totalHT * 1.19m;

            await _context.SaveChangesAsync();

            // Recharger la commande
            var updatedCommande = await _context.Commandes
                .Include(c => c.Client)
                .Include(c => c.Lignes_de_commande)
                .ThenInclude(l => l.Produit)
                .FirstOrDefaultAsync(c => c.Identifiant == id);

            return MapToCommandeDTO(updatedCommande);
        }

        public async Task DeleteCommandeAsync(int id)
        {
            var commande = await _context.Commandes
                .FirstOrDefaultAsync(c => c.Identifiant == id);

            if (commande == null)
                throw new Exception($"Commande avec l'identifiant {id} non trouvée");

            // Ne peut supprimer que les commandes en brouillon ou annulées
            if (commande.Statut_de_la_commande == "Validée")
                throw new Exception("Impossible de supprimer une commande validée");

            _context.Commandes.Remove(commande);
            await _context.SaveChangesAsync();
        }

        public async Task ValidateCommandeAsync(int id)
        {
            var commande = await _context.Commandes
                .Include(c => c.Lignes_de_commande)
                .ThenInclude(l => l.Produit)
                .FirstOrDefaultAsync(c => c.Identifiant == id);

            if (commande == null)
                throw new Exception($"Commande avec l'identifiant {id} non trouvée");

            if (commande.Statut_de_la_commande != "Brouillon")
                throw new Exception("Seules les commandes en brouillon peuvent être validées");

            // Vérifier le stock pour chaque produit
            foreach (var ligne in commande.Lignes_de_commande)
            {
                if (ligne.Quantité > ligne.Produit.Quantité_en_stock)
                    throw new Exception($"Stock insuffisant pour le produit {ligne.Produit.Nom_du_produit}");
            }

            // Mettre à jour le stock
            foreach (var ligne in commande.Lignes_de_commande)
            {
                ligne.Produit.Quantité_en_stock -= ligne.Quantité;
            }

            commande.Statut_de_la_commande = "Validée";
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Génère un numéro de commande unique
        /// </summary>
        private async Task<string> GenerateOrderNumberAsync()
        {
            var datePart = DateTime.UtcNow.ToString("yyyyMMdd");
            var lastOrder = await _context.Commandes
                .Where(c => c.Numéro_de_commande.StartsWith($"ORD-{datePart}"))
                .OrderByDescending(c => c.Numéro_de_commande)
                .FirstOrDefaultAsync();

            int sequence = 1;
            if (lastOrder != null)
            {
                var sequenceStr = lastOrder.Numéro_de_commande.Split('-')[2];
                sequence = int.Parse(sequenceStr) + 1;
            }

            return $"ORD-{datePart}-{sequence:D6}";
        }

        /// <summary>
        /// Mappe une entité Commande vers CommandeDTO
        /// </summary>
        private CommandeDTO MapToCommandeDTO(Commande commande)
        {
            return new CommandeDTO
            {
                Identifiant = commande.Identifiant,
                Numéro_de_commande = commande.Numéro_de_commande,
                Client_associé = commande.Client_associé,
                ClientNom = commande.Client != null ? $"{commande.Client.Nom} {commande.Client.Prénom_ou_raison_sociale}" : "Client inconnu",
                Date_de_commande = commande.Date_de_commande,
                Statut_de_la_commande = commande.Statut_de_la_commande,
                Total_HT = commande.Total_HT,
                Total_TTC = commande.Total_TTC,
                Lignes_de_commande = commande.Lignes_de_commande.Select(l => new LigneDeCommandeDTO
                {
                    Identifiant = l.Identifiant,
                    Produit_Identifiant = l.Produit_Identifiant,
                    ProduitNom = l.Produit?.Nom_du_produit ?? "Produit inconnu",
                    ProduitReference = l.Produit?.Référence ?? "Réf. inconnue",
                    Quantité = l.Quantité,
                    Prix_unitaire = l.Prix_unitaire,
                    Total_ligne = l.Total_ligne
                }).ToList()
            };
        }
    }
}