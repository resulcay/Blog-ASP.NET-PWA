using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
	public class CategoryController : Controller
	{
		readonly CategoryManager _manager = new(new EfCategoryRepository());

		public IActionResult Index()
		{
			var values = _manager.GetEntities();
			return View(values);
		}
	}
}
