using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private IConfiguration _configuration;

        public CustomersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _configuration["data"];
            return Ok(string.Concat(data," - ", 
                new List<string> { 
                    "Hilmi CELAYİR",
                    "Saniye YILDIZ", 
                    "Faruk KARAASLAN", 
                    "Ramazan TAŞÖRER", 
                    "Oğuzhan DİLEK" 
                }));
        }
    }
}
