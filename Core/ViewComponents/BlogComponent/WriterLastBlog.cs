using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.BlogComponent
{
    public class WriterLastBlog : ViewComponent
    {
        readonly BlogManager manager = new(new EfBlogRepository());

        public IViewComponentResult Invoke(int id)
        {
            ViewBag.i = id;
            var blog = manager.GetEntityById(id);
            var values = manager.GetBlogListByWriter(blog.WriterID, false);

            return View(values);
        }
    }
}
