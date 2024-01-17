using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    public class DashboardController : Controller
    {
        readonly BlogManager blogManager = new(new EfBlogRepository());
        readonly CategoryManager categoryManager = new(new EfCategoryRepository());
        readonly WriterManager writerManager = new(new EfWriterRepository());

        public IActionResult Index()
        {
            // TODO: will impact performance if there are many.
            ViewBag.totalBlogCount = blogManager.GetEntities().Count;
            ViewBag.categoryCount = categoryManager.GetEntities().Count;

            var userMail = User.Identity.Name;
            var writerID = writerManager.GetWriterIDBySession(userMail);

            ViewBag.totalBlogCountByWriter = blogManager.TotalBlogCountByWriter(writerID);

            return View();
        }
    }
}
