using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Core.ViewComponents.BlogComponent
{
    public class BlogListDashboard : ViewComponent
    {
        readonly BlogManager manager = new(new EfBlogRepository());

        public IViewComponentResult Invoke()
        {
            var values = manager.GetBlogListWithCategory(10);
            return View(values);
        }
    }
}
