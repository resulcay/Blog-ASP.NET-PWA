using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        private readonly BlogManager _blogManager = new(new EfBlogRepository());

        public IActionResult Index()
        {
            var blogs = _blogManager.GetDetailedBlogList();
            return View(blogs);
        }
    }
}
