using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Context;
using ProductAPI.Interface;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly MyDbContext _dbContext;
        protected readonly ICapHelper _capHepler;

        public BaseController(MyDbContext dbContext, ICapHelper capHepler)
        {
            _dbContext = dbContext;
            _capHepler = capHepler;
        }
    }
}
