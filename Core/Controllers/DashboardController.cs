using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Core.Controllers
{
    public class DashboardController : Controller
    {
        readonly BlogManager blogManager = new(new EfBlogRepository());
        readonly CategoryManager categoryManager = new(new EfCategoryRepository());
        readonly WriterManager writerManager = new(new EfWriterRepository());
        readonly Context context = new();

        public IActionResult Index()
        {
            // TODO: will impact performance if there are many.
            ViewBag.totalBlogCount = blogManager.GetEntities().Count;
            ViewBag.categoryCount = categoryManager.GetEntities().Count;

			var userName = User.Identity.Name;
			var userMail = context.Users.Where(x => x.UserName == userName).Select(x => x.Email).FirstOrDefault();
			var writerID = writerManager.GetWriterIDBySession(userMail);
			var writer = writerManager.GetEntityById(writerID);

			ViewBag.totalBlogCountByWriter = blogManager.TotalBlogCountByWriter(writerID);

            return View();
        }
    }
}
