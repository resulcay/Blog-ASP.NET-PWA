using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    public class AboutController : Controller
    {
        readonly AboutManager manager = new(new EfAboutRepository());

        public IActionResult Index()
        {
            var values = manager.GetEntities();
            return View(values);
        }

        public PartialViewResult SocialMediaAbout() 
        {
            return PartialView();
        }
    }
}
