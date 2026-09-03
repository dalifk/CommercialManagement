using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommercialManagement.Application.DTOs;
using CommercialManagement.Application.Services.Interfaces;

namespace CommercialManagement.API.Controllers
{
    /// <summary>
    /// Contrôleur pour la gestion des clients
    /// Routes: /api/clients
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Récupère la liste de tous les clients
        /// GET: /api/clients
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                var clients = await _clientService.GetAllClientsAsync();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la récupération des clients", error = ex.Message });
            }
        }

        /// <summary>
        /// Récupère un client par son identifiant
        /// GET: /api/clients/{id}
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            try
            {
                var client = await _clientService.GetClientByIdAsync(id);
                return Ok(client);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Crée un nouveau client
        /// POST: /api/clients
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientDTO createClientDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var client = await _clientService.CreateClientAsync(createClientDto);
                return CreatedAtAction(nameof(GetClientById), new { id = client.Identifiant }, client);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Met à jour un client existant
        /// PUT: /api/clients/{id}
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] UpdateClientDTO updateClientDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var client = await _clientService.UpdateClientAsync(id, updateClientDto);
                return Ok(client);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Supprime un client
        /// DELETE: /api/clients/{id}
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                await _clientService.DeleteClientAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}