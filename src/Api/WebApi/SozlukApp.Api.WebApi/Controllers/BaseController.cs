using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SozlukApp.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public Guid UserId => Guid.NewGuid(); //new(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}
