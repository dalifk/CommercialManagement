using Microsoft.EntityFrameworkCore;
using CommercialManagement.Application.DTOs;
using CommercialManagement.Application.Services.Interfaces;
using CommercialManagement.Domain.Entities;
using CommercialManagement.Infrastructure.Data;

namespace CommercialManagement.Application.Services
{
    /// <summary>
    /// Service de gestion des produits
    /// </summary>
    public class ProduitService : IProduitService
    {
        private readonly ApplicationDbContext _context;

        public ProduitService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProduitDTO>> GetAllProduitsAsync()
        {
            var produits = await _context.Produits
                .OrderBy(p => p.Nom_du_produit)
                .ToListAsync();

            return produits.Select(p => new ProduitDTO
            {
                Identifiant = p.Identifiant,
                Référence = p.Référence,
                Nom_du_produit = p.Nom_du_produit,
                Description = p.Description,
                Prix_unitaire_HT = p.Prix_unitaire_HT,
                Quantité_en_stock = p.Quantité_en_stock,
                Date_de_création = p.Date_de_création
            });
        }

        public async Task<ProduitDTO> GetProduitByIdAsync(int id)
        {
            var produit = await _context.Produits
                .FirstOrDefaultAsync(p => p.Identifiant == id);

            if (produit == null)
                throw new Exception($"Produit avec l'identifiant {id} non trouvé");

            return new ProduitDTO
            {
                Identifiant = produit.Identifiant,
                Référence = produit.Référence,
                Nom_du_produit = produit.Nom_du_produit,
                Description = produit.Description,
                Prix_unitaire_HT = produit.Prix_unitaire_HT,
                Quantité_en_stock = produit.Quantité_en_stock,
                Date_de_création = produit.Date_de_création
            };
        }

        public async Task<ProduitDTO> CreateProduitAsync(CreateProduitDTO createProduitDto)
        {
            // Vérifier si la référence existe déjà
            var existingProduit = await _context.Produits
                .FirstOrDefaultAsync(p => p.Référence == createProduitDto.Référence);

            if (existingProduit != null)
                throw new Exception($"Un produit avec la référence {createProduitDto.Référence} existe déjà");

            var produit = new Produit
            {
                Référence = createProduitDto.Référence,
                Nom_du_produit = createProduitDto.Nom_du_produit,
                Description = createProduitDto.Description,
                Prix_unitaire_HT = createProduitDto.Prix_unitaire_HT,
                Quantité_en_stock = createProduitDto.Quantité_en_stock,
                Date_de_création = DateTime.UtcNow
            };

            _context.Produits.Add(produit);
            await _context.SaveChangesAsync();

            return new ProduitDTO
            {
                Identifiant = produit.Identifiant,
                Référence = produit.Référence,
                Nom_du_produit = produit.Nom_du_produit,
                Description = produit.Description,
                Prix_unitaire_HT = produit.Prix_unitaire_HT,
                Quantité_en_stock = produit.Quantité_en_stock,
                Date_de_création = produit.Date_de_création
            };
        }

        public async Task<ProduitDTO> UpdateProduitAsync(int id, UpdateProduitDTO updateProduitDto)
        {
            var produit = await _context.Produits
                .FirstOrDefaultAsync(p => p.Identifiant == id);

            if (produit == null)
                throw new Exception($"Produit avec l'identifiant {id} non trouvé");

            // Vérifier si la référence est déjà utilisée par un autre produit
            var existingProduit = await _context.Produits
                .FirstOrDefaultAsync(p => p.Référence == updateProduitDto.Référence && p.Identifiant != id);

            if (existingProduit != null)
                throw new Exception($"Un produit avec la référence {updateProduitDto.Référence} existe déjà");

            produit.Référence = updateProduitDto.Référence;
            produit.Nom_du_produit = updateProduitDto.Nom_du_produit;
            produit.Description = updateProduitDto.Description;
            produit.Prix_unitaire_HT = updateProduitDto.Prix_unitaire_HT;
            produit.Quantité_en_stock = updateProduitDto.Quantité_en_stock;

            await _context.SaveChangesAsync();

            return new ProduitDTO
            {
                Identifiant = produit.Identifiant,
                Référence = produit.Référence,
                Nom_du_produit = produit.Nom_du_produit,
                Description = produit.Description,
                Prix_unitaire_HT = produit.Prix_unitaire_HT,
                Quantité_en_stock = produit.Quantité_en_stock,
                Date_de_création = produit.Date_de_création
            };
        }

        public async Task DeleteProduitAsync(int id)
        {
            var produit = await _context.Produits
                .Include(p => p.Lignes_de_commande)
                .FirstOrDefaultAsync(p => p.Identifiant == id);

            if (produit == null)
                throw new Exception($"Produit avec l'identifiant {id} non trouvé");

            // Vérifier si le produit est utilisé dans des commandes
            if (produit.Lignes_de_commande.Any())
                throw new Exception("Impossible de supprimer un produit qui est utilisé dans des commandes");

            _context.Produits.Remove(produit);
            await _context.SaveChangesAsync();
        }
    }
}