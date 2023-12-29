using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Context;
using ProductAPI.Helper;
using ProductAPI.Interface;
using ProductAPI.Model;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        public ProductsController(MyDbContext dbContext, ICapHelper capHepler) 
            : base(dbContext, capHepler)
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            var products = _dbContext.Set<Product>().ToList();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            _dbContext.Add(product);
            _dbContext.SaveChanges();

            await _capHepler.ExecuteWithTransactionAsync("add-product-helper", product);

            return Ok(product);
        }

        [CapSubscribe("add-customer-helper")]
        public async Task AddCustomer(Customer customer)
        {
            try
            {
                await _dbContext.AddAsync(customer);
                await _dbContext.SaveChangesAsync();
         
                Console.WriteLine("Yeni müşteri eklendi");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }
    }
}
