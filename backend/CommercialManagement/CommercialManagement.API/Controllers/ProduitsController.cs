using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommercialManagement.Application.DTOs;
using CommercialManagement.Application.Services.Interfaces;

namespace CommercialManagement.API.Controllers
{
    /// <summary>
    /// Contrôleur pour la gestion des produits
    /// Routes: /api/produits
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProduitsController : ControllerBase
    {
        private readonly IProduitService _produitService;

        public ProduitsController(IProduitService produitService)
        {
            _produitService = produitService;
        }

        /// <summary>
        /// Récupère la liste de tous les produits
        /// GET: /api/produits
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllProduits()
        {
            try
            {
                var produits = await _produitService.GetAllProduitsAsync();
                return Ok(produits);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la récupération des produits", error = ex.Message });
            }
        }

        /// <summary>
        /// Récupère un produit par son identifiant
        /// GET: /api/produits/{id}
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduitById(int id)
        {
            try
            {
                var produit = await _produitService.GetProduitByIdAsync(id);
                return Ok(produit);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Crée un nouveau produit
        /// POST: /api/produits
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateProduit([FromBody] CreateProduitDTO createProduitDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var produit = await _produitService.CreateProduitAsync(createProduitDto);
                return CreatedAtAction(nameof(GetProduitById), new { id = produit.Identifiant }, produit);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Met à jour un produit existant
        /// PUT: /api/produits/{id}
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduit(int id, [FromBody] UpdateProduitDTO updateProduitDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var produit = await _produitService.UpdateProduitAsync(id, updateProduitDto);
                return Ok(produit);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Supprime un produit
        /// DELETE: /api/produits/{id}
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduit(int id)
        {
            try
            {
                await _produitService.DeleteProduitAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}