using CustomerAPI.Context;
using CustomerAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
       protected readonly MyDbContext _dbContext;
        protected readonly ICapHelper _capHelper;

        public BaseController(MyDbContext dbContext, ICapHelper capHelper)
        {
            _dbContext = dbContext;
            _capHelper = capHelper;
        }
    }
}
