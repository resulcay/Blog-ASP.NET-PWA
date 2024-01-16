using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class ErrorPageController : Controller
    {
        //int code
        public IActionResult Error1() => View();
    }
}
