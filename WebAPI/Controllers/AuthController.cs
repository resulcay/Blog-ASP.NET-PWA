using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataAccessLayer;

namespace WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("login")]
        public IActionResult Login()
        {
            BuildToken token = new();
            return Created("", token.CreateToken());
        }

        [Authorize]
        [HttpGet("checkauth")]
        public IActionResult DemoPage()
        {
            return Ok("Giriş Başarılı");
        }
    }
}
