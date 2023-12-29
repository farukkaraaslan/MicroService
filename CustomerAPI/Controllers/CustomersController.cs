using CustomerAPI.Context;
using CustomerAPI.Helper;
using CustomerAPI.Interfaces;
using CustomerAPI.Models;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : BaseController
    {
        public CustomersController(MyDbContext dbContext, ICapHelper capHelper) : base(dbContext, capHelper)
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            var customers = _dbContext.Set<Customer>().ToList();
            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Customer customer)
        {
           
            _dbContext.Add(customer);
           _dbContext.SaveChanges();

            await _capHelper.ExecuteWithTransactionAsync("add-customer-helper", customer);

            return Ok(customer);
        }

        [CapSubscribe("add-product-helper")]
        public async Task AddProduct(Product product)
        {
            try
            {
                await _dbContext.AddAsync(product);
                await _dbContext.SaveChangesAsync();

                Console.WriteLine("Yeni ürün eklendi");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }

    }
}
