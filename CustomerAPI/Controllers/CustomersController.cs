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

            //var uri = new Uri("http://localhost:5001/api/products/addcustomer");

            //var jsonContent = JsonConvert.SerializeObject(customer);

            //var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            //_serviceCallHelper.Post(uri, HttpMethod.Post, httpContent);

            //using var transaction= _dbContext.Database.BeginTransaction(_capPublisher,autoCommit:true);

            //await _capPublisher.PublishAsync<Customer>("customer-add", customer);

            await _capHelper.ExecuteWithTransactionAsync("add-customer-helper", customer);

            return Ok(customer);
        }

        [CapSubscribe("product-add")]

        public void GetCustomer(Product product)
        {
            Console.WriteLine(product.Name);
        }

        //[HttpPost("products")]
        //public async Task<IActionResult> AddProduct(Product product)
        //{
        //    var uri = new Uri("http://localhost:5001/api/products");
        //    var jsonContent = JsonConvert.SerializeObject(product);

        //    var mediaType = new MediaTypeHeaderValue("application/json");
        //    var httpContent = new StringContent(jsonContent, Encoding.UTF8, mediaType.ToString());
        //    var response = await _serviceCallHelper.Post(uri, HttpMethod.Post, httpContent);
        //    return Ok(response);
          
    
        //}
    }
}
