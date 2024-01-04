using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.BlogComponent
{
	public class WriterLastBlog: ViewComponent
	{
		BlogManager manager = new(new EfBlogRepository());

		public IViewComponentResult Invoke(int id)
		{
			ViewBag.i = id;

			var blog = manager.GetById(id);
			var values = manager.GetBlogListByWriter(blog.WriterID);
			return View(values);
		}
	}
}
