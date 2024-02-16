using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    public class AboutController : Controller
    {
        private readonly AboutManager _aboutMmanager = new(new EfAboutRepository());

        public IActionResult Index()
        {
            var values = _aboutMmanager.GetEntities();
            return View(values);
        }
    }
}
