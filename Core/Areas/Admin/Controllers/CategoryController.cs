using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly CategoryManager _categoryManager = new(new EfCategoryRepository());

        public IActionResult Index(int page = 1)
        {
            var categories = _categoryManager.GetEntities().ToPagedList(page, 3);
            return View(categories);
        }

        public IActionResult ActivateCategory(int id)
        {
            Category category = _categoryManager.GetEntityById(id);

            category.CategoryStatus = true;
            _categoryManager.UpdateEntity(category);

            return RedirectToAction("Index", "Category");
        }

        public IActionResult DeactivateCategory(int id)
        {
            Category category = _categoryManager.GetEntityById(id);

            category.CategoryStatus = false;
            _categoryManager.UpdateEntity(category);

            return RedirectToAction("Index", "Category");
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var category = _categoryManager.GetEntityById(id);
            return View(category);
        }


        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            CategoryValidator categoryValidator = new();
            ValidationResult result = categoryValidator.Validate(category);

            if (result.IsValid)
            {
                _categoryManager.UpdateEntity(category);
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
                _categoryManager.AddEntity(category);

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
            var category = _categoryManager.GetEntityById(id);
            _categoryManager.DeleteEntity(category);

            return RedirectToAction("Index", "Category");
        }
    }
}
