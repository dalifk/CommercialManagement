using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CommercialManagement.Infrastructure.Data;

namespace CommercialManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Test()
        {
            try
            {
                var canConnect = _context.Database.CanConnect();
                return Ok(new
                {
                    Message = "API is working!",
                    Database = canConnect ? "Connected" : "Not Connected"
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Message = "API is working!",
                    Database = "Connection Error: " + ex.Message
                });
            }
        }
    }
}