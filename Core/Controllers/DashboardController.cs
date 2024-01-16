using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    public class DashboardController : Controller
    {
        readonly BlogManager blogManager = new(new EfBlogRepository());
        readonly CategoryManager categoryManager = new(new EfCategoryRepository());

        public IActionResult Index()
        {
            ViewBag.totalBlogCount = blogManager.TotalBlogCount();
            ViewBag.totalBlogCountByWriter = blogManager.TotalBlogCountByWriter(1);
            ViewBag.categoryCount = categoryManager.GetEntities().Count;

            return View();
        }
    }
}
