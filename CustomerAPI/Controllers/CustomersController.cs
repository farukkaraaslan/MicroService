using CustomerAPI.Context;
using CustomerAPI.Interfaces;
using CustomerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly MyDbContext _dbContext;
        private IConfiguration _configuration;
        private readonly IServiceCallHelper _serviceCallHelper;
        public CustomersController(IServiceCallHelper serviceCallHelper,IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = new MyDbContext(); 
            _serviceCallHelper = serviceCallHelper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var customers = _dbContext.Set<Customer>().ToList();
            return Ok(customers);
        }

        [HttpPost]
        public IActionResult Add(Customer customer)
        {
           
            _dbContext.Add(customer);
           _dbContext.SaveChanges();

            var uri = new Uri("http://localhost:5001/api/products/addcustomer");
            var jsonContent = JsonConvert.SerializeObject(customer);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            _serviceCallHelper.Post(uri, HttpMethod.Post, httpContent);

            return Ok(customer);
        }

        [HttpPost("products")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            var uri = new Uri("http://localhost:5001/api/products");
            var jsonContent = JsonConvert.SerializeObject(product);

            var mediaType = new MediaTypeHeaderValue("application/json");
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, mediaType.ToString());
            var response = await _serviceCallHelper.Post(uri, HttpMethod.Post, httpContent);
            return Ok(response);
          
    
        }
    }
}
