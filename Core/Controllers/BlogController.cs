using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        readonly BlogManager manager = new(new EfBlogRepository());

        public IActionResult Index()
        {
            var values = manager.GetBlogListWithCategory();
            return View(values);
        }

        public IActionResult BlogReadAll(int id) 
        {
            ViewBag.i = id;
            var values = manager.GetBlogById(id);
            return View(values);
        }
	}
}
