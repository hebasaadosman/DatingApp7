
using API.Data;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))] // Add the LogUserActivity class to the services container.
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : Controller
    {
         private readonly DataContext _context;

           
       
    }
}