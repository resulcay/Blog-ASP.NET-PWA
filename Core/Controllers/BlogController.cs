using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogManager _blogManager = new(new EfBlogRepository());
        private readonly CategoryManager _categoryManager = new(new EfCategoryRepository());
        private readonly WriterManager _writerManager = new(new EfWriterRepository());
        private readonly UserManager<User> _userManager;

        public BlogController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var values = _blogManager.GetBlogListWithCategory(null);
            return View(values);
        }

        [AllowAnonymous]
        public IActionResult BlogReadAll(int id)
        {
            ViewBag.i = id;

            var value = _blogManager.GetEntityById(id);
            if (!value.BlogStatus)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(value);
        }

        public async Task<IActionResult> BlogListByWriter()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            string userId = await _userManager.GetUserIdAsync(user);
            var writer = _writerManager.GetWriterBySession(userId);

            var values = _blogManager.GetBlogListByWriter(writer.WriterID, true);

            return View(values);
        }

        [HttpGet]
        public IActionResult BlogAdd()
        {
            PopulateCategoriesDropdown();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BlogAdd(Blog blog)
        {
            BlogValidator blogValidator = new();
            ValidationResult result = blogValidator.Validate(blog);

            if (result.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                string userId = await _userManager.GetUserIdAsync(user);
                var writer = _writerManager.GetWriterBySession(userId);

                blog.BlogStatus = true;
                blog.BlogCreatedAt = DateTime.Now;
                blog.WriterID = writer.WriterID;
                _blogManager.AddEntity(blog);

                return RedirectToAction("BlogListByWriter", "Blog");
            }
            else
            {
                PopulateCategoriesDropdown();

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }

        public IActionResult DeleteBlog(int id)
        {
            var value = _blogManager.GetEntityById(id);
            _blogManager.DeleteEntity(value);

            return RedirectToAction("BlogListByWriter");
        }

        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            var value = _blogManager.GetEntityById(id);
            PopulateCategoriesDropdown();

            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> EditBlog(Blog blog)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            string userId = await _userManager.GetUserIdAsync(user);
            var writer = _writerManager.GetWriterBySession(userId);

            blog.WriterID = writer.WriterID;
            _blogManager.UpdateEntity(blog);

            return RedirectToAction("BlogListByWriter");
        }

        private void PopulateCategoriesDropdown()
        {
            List<SelectListItem> categories = (from x in _categoryManager.GetEntities()
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.CategoryID.ToString()
                                               }).ToList();
            ViewBag.categories = categories;
        }
    }
}
