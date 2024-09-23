using Microsoft.AspNetCore.Mvc;

namespace API_Gateway_ocelot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OcelotController : ControllerBase
    {
       

        private readonly ILogger<OcelotController> _logger;

        public OcelotController(ILogger<OcelotController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Info")]
        public IActionResult Info()
        {
            return Ok("Is Ruuning");
        }
        [HttpGet("Test")]
        public IActionResult Test()
        {
            return Ok("Is Working");
        }
    }
}