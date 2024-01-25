using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        readonly BlogManager blogManager = new(new EfBlogRepository());

        public IActionResult Index()
        {
            var blogs = blogManager.GetDetailedBlogList();
            return View(blogs);
        }
    }
}
