using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new List<string> { "Hilmi CELAYİR", "Saniye YILDIZ", "Faruk KARAASLAN", "Ramazan TAŞÖRER","Oğuzhan DİLEK" });
        }
    }
}
