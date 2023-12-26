using CustomerAPI.Context;
using CustomerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly MyDbContext _dbContext;
        private IConfiguration _configuration;

        public CustomersController(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = new MyDbContext(); 
        }

        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            _dbContext.Add(customer);
           _dbContext.SaveChanges();
            return Ok(customer);
        }

        [HttpGet]
        public IActionResult Get()
        {
          var customer = _dbContext.Set<Customer>();
            return Ok(customer);
        }
    }
}
