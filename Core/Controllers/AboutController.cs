using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    public class AboutController : Controller
    {
        private readonly AboutManager _manager = new(new EfAboutRepository());

        public IActionResult Index()
        {
            var values = _manager.GetEntities();
            return View(values);
        }
    }
}
