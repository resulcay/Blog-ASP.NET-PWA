using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataAccessLayer;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultsController : ControllerBase
    {
        [HttpGet("[action]")]
        public IActionResult Login()
        {
            BuildToken token = new();
            return Created("", token.CreateToken());
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult DemoPage()
        {
            return Ok("Demo Girişi Başarılı");
        }
    }
}
