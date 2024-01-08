using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CoreDemo.ViewComponents.Category
{
	public class CategoryList : ViewComponent
	{
		public IViewComponentResult Invoke() 
		{
			using var context = new Context();
			var categoriesWithBlogCounts = context.Categories.Include(x => x.Blogs).OrderByDescending(c => c.Blogs.Count).ToList();

			return View(categoriesWithBlogCounts);
		}
	}
}
