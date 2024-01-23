using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        readonly CategoryManager categoryManager = new(new EfCategoryRepository());

        public IActionResult Index(int page = 1)
        {
            var categories = categoryManager.GetEntities().ToPagedList(page, 3);
            return View(categories);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            CategoryValidator categoryValidator = new();
            ValidationResult result = categoryValidator.Validate(category);

            if (result.IsValid)
            {
                category.CategoryStatus = true;
                categoryManager.AddEntity(category);

                return RedirectToAction("Index", "Category");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }

        public IActionResult DeleteCategory(int id)
        {
            var category = categoryManager.GetEntityById(id);
            categoryManager.DeleteEntity(category);

            return RedirectToAction("Index", "Category");
        }
    }
}
