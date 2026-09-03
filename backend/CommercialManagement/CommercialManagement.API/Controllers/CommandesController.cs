using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommercialManagement.Application.DTOs;
using CommercialManagement.Application.Services.Interfaces;

namespace CommercialManagement.API.Controllers
{
    /// <summary>
    /// Contrôleur pour la gestion des commandes
    /// Routes: /api/commandes
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CommandesController : ControllerBase
    {
        private readonly ICommandeService _commandeService;

        public CommandesController(ICommandeService commandeService)
        {
            _commandeService = commandeService;
        }

        /// <summary>
        /// Récupère la liste de toutes les commandes
        /// GET: /api/commandes
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllCommandes()
        {
            try
            {
                var commandes = await _commandeService.GetAllCommandesAsync();
                return Ok(commandes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la récupération des commandes", error = ex.Message });
            }
        }

        /// <summary>
        /// Récupère une commande par son identifiant avec toutes ses lignes
        /// GET: /api/commandes/{id}
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommandeById(int id)
        {
            try
            {
                var commande = await _commandeService.GetCommandeByIdAsync(id);
                return Ok(commande);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Crée une nouvelle commande
        /// POST: /api/commandes
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateCommande([FromBody] CreateCommandeDTO createCommandeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var commande = await _commandeService.CreateCommandeAsync(createCommandeDto);
                return CreatedAtAction(nameof(GetCommandeById), new { id = commande.Identifiant }, commande);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Met à jour une commande existante
        /// PUT: /api/commandes/{id}
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCommande(int id, [FromBody] UpdateCommandeDTO updateCommandeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var commande = await _commandeService.UpdateCommandeAsync(id, updateCommandeDto);
                return Ok(commande);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Supprime une commande
        /// DELETE: /api/commandes/{id}
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommande(int id)
        {
            try
            {
                await _commandeService.DeleteCommandeAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Valide une commande (met à jour le stock)
        /// PATCH: /api/commandes/{id}/validate
        /// </summary>
        [HttpPatch("{id}/validate")]
        public async Task<IActionResult> ValidateCommande(int id)
        {
            try
            {
                await _commandeService.ValidateCommandeAsync(id);
                return Ok(new { message = $"Commande {id} validée avec succès", status = "Validée" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}