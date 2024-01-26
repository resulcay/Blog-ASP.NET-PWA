using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
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
    public class BlogController : Controller
    {
        readonly BlogManager blogManager = new(new EfBlogRepository());
        readonly CategoryManager categoryManager = new(new EfCategoryRepository());
        readonly WriterManager writerManager = new(new EfWriterRepository());
        readonly Context context = new();

        [AllowAnonymous]
        public IActionResult Index()
        {
            var values = blogManager.GetBlogListWithCategory(null);
            return View(values);
        }

        [AllowAnonymous]
        public IActionResult BlogReadAll(int id)
        {
            ViewBag.i = id;

            var value = blogManager.GetEntityById(id);
            if (!value.BlogStatus)
            {
                return RedirectToAction("Error","Home");
            }

            return View(value);
        }

        public IActionResult BlogListByWriter()
        {
            var userName = User.Identity.Name;
            var userMail = context.Users.Where(x => x.UserName == userName).Select(x => x.Email).FirstOrDefault();
            var writerID = writerManager.GetWriterIDBySession(userMail);
            var values = blogManager.GetBlogListByWriter(writerID, true);

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
                var userName = User.Identity.Name;
                var userMail = context.Users.Where(x => x.UserName == userName).Select(x => x.Email).FirstOrDefault();
                var writerID = writerManager.GetWriterIDBySession(userMail);

                blog.BlogStatus = true;
                blog.BlogCreatedAt = DateTime.Now;
                blog.WriterID = writerID;
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
            var userName = User.Identity.Name;
            var userMail = context.Users.Where(x => x.UserName == userName).Select(x => x.Email).FirstOrDefault();
            var writerID = writerManager.GetWriterIDBySession(userMail);

            blog.WriterID = writerID;
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
