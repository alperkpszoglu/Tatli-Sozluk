using Microsoft.AspNetCore.Mvc;

namespace SozlukApp.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetValue() {
            throw new Exception("yeni bir istisna firlattim");
            return Ok();
        }

    }
}
