using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    public class ErrorPageController : Controller
    {
#pragma warning disable IDE0060 // Remove unused parameter
        public IActionResult Error1(int code) => View();
#pragma warning restore IDE0060 // Remove unused parameter
    }
}
