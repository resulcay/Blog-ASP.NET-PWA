using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.BlogComponent
{
    public class WriterLastBlog : ViewComponent
    {
        private readonly BlogManager _blogManager = new(new EfBlogRepository());

        public IViewComponentResult Invoke(int id)
        {
            ViewBag.i = id;
            var blog = _blogManager.GetEntityById(id);
            var values = _blogManager.GetBlogListByWriter(blog.WriterID, false);
            return View(values);
        }
    }
}
