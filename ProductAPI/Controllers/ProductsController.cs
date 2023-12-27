using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Context;
using ProductAPI.Model;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _dbContext;

        public ProductsController(MyDbContext dbContext)
        {
            _dbContext = new MyDbContext();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var products = _dbContext.Set<Product>().ToList();
            return Ok(products);
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            _dbContext.Add(product);
            _dbContext.SaveChanges();
            return Ok(product);
        }

        [HttpPost("addcustomer")]
        public void AddCustomer(Customer customer)
        {
            try
            {
                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();
                Console.WriteLine("Yeni müşteri eklendi");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }
    }
}
