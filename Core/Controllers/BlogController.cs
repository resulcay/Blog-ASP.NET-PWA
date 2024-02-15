using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using Core.Models;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
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

            if (value == null || !value.BlogStatus)
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
        public async Task<IActionResult> BlogAdd(BlogAddViewModel model)
        {
            bool isJpg = string.Equals(model.BlogImage?.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase);
            bool isJpeg = string.Equals(model.BlogImage?.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase);
            bool isPng = string.Equals(model.BlogImage?.ContentType, "image/png", StringComparison.OrdinalIgnoreCase);

            if (model.BlogImage == null)
            {
                ModelState.AddModelError("BlogImage", "Lütfen bir profil resmi yükleyiniz.");
            }
            else if (!isJpg && !isJpeg && !isPng)
            {
                ModelState.AddModelError("BlogImage", "Lütfen geçerli bir profil resmi yükleyiniz.");
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            string userId = await _userManager.GetUserIdAsync(user);
            var writer = _writerManager.GetWriterBySession(userId);

            var extension = Path.GetExtension(model.BlogImage?.FileName);
            var newImageName = Guid.NewGuid() + extension;

            Blog blog = new()
            {
                BlogTitle = model.BlogTitle,
                BlogContent = model.BlogContent,
                BlogStatus = true,
                BlogCreatedAt = DateTime.Now,
                BlogImage = "/WriterBlogFiles/" + newImageName,
                CategoryID = model.CategoryID,
                WriterID = writer.WriterID,
            };

            BlogValidator blogValidator = new();
            ValidationResult result = blogValidator.Validate(blog);

            if (result.IsValid && model.BlogImage != null && (isJpg || isJpeg || isPng))
            {
                var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterBlogFiles/");
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                var location = Path.Combine(directory + newImageName);
                var stream = new FileStream(location, FileMode.Create);
                model.BlogImage.CopyTo(stream);

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
