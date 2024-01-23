using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Core.ViewComponents.BlogComponent
{
    public class LastBlogs : ViewComponent
    {
        readonly BlogManager manager = new(new EfBlogRepository());

        public IViewComponentResult Invoke()
        {
            var values = manager.GetLastBlogs();
            return View(values);
        }
    }
}
