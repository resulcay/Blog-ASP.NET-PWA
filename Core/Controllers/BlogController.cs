using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        readonly BlogManager blogManager = new(new EfBlogRepository());
        readonly CategoryManager categoryManager = new(new EfCategoryRepository());

        public IActionResult Index()
        {
            var values = blogManager.GetBlogListWithCategory(null);
            return View(values);
        }

        public IActionResult BlogReadAll(int id)
        {
            ViewBag.i = id;
            var value = blogManager.GetEntityById(id);
            return View(value);
        }

        public IActionResult BlogListByWriter()
        {
            var values = blogManager.GetBlogListByWriter(1, true);
            return View(values);
        }

        [HttpGet]
        public IActionResult BlogAdd()
        {
            PopulateCategoriesDropdown();
            return View();
        }

        [HttpPost]
        public IActionResult BlogAdd(Blog blog)
        {
            BlogValidator blogValidator = new();
            ValidationResult result = blogValidator.Validate(blog);

            if (result.IsValid)
            {
                blog.BlogStatus = true;
                blog.BlogCreatedAt = DateTime.Now;
                blog.WriterID = 1;
                blogManager.AddEntity(blog);

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
            var value = blogManager.GetEntityById(id);
            blogManager.DeleteEntity(value);

            return RedirectToAction("BlogListByWriter");
        }

        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            var value = blogManager.GetEntityById(id);
            PopulateCategoriesDropdown();

            return View(value);
        }

        [HttpPost]
        public IActionResult EditBlog(Blog blog)
        {
            blog.WriterID = 1;
            blogManager.UpdateEntity(blog);

            return RedirectToAction("BlogListByWriter");
        }

        private void PopulateCategoriesDropdown()
        {
            List<SelectListItem> categories = (from x in categoryManager.GetEntities()
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.CategoryID.ToString()
                                               }).ToList();
            ViewBag.categories = categories;
        }
    }
}
