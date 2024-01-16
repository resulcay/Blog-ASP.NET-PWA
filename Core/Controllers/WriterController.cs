using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using Core.Models;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace Core.Controllers
{
    public class WriterController : Controller
    {
        readonly WriterManager writerManager = new(new EfWriterRepository());
        [Authorize]

        public IActionResult Index()
        {
            var userMail = User.Identity.Name;
            ViewBag.v = userMail;

            Context context = new();
            var writerInfo = context.Writers.Where(x => x.WriterMail == userMail).Select(y => y.WriterName).FirstOrDefault();
            ViewBag.v2 = writerInfo;
            return View();
        }

        public PartialViewResult WriterNavBarPartial()
        {
            return PartialView();
        }

        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }

        [HttpGet]
        public IActionResult WriterEditProfile()
        {
            var userMail = User.Identity.Name;
            var writerID = writerManager.GetWriterIDBySession(userMail);
            var writer = writerManager.GetEntityById(writerID);

            return View(writer);
        }

        [HttpPost]
        public IActionResult WriterEditProfile(Writer writer, string confirmPassword, dynamic image)
        {
            if (writer.WriterPassword != confirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Şifreler eşleşmiyor.");
            }

            if (image == null)
            {
                ModelState.AddModelError("WriterImage", "Lütfen bir profil resmi yükleyiniz.");
            }

            WriterValidator writerValidator = new();
            ValidationResult result = writerValidator.Validate(writer);

            if (result.IsValid && ModelState.IsValid)
            {
                writerManager.UpdateEntity(writer);
                return RedirectToAction("Index", "Dashboard");
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

        #region WriterAddBlog
        [AllowAnonymous]
        [HttpGet]
        public IActionResult WriterAdd()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult WriterAdd(AddProfileImage imageObject, string confirmPassword)
        {
            if (imageObject.WriterPassword != confirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Şifreler eşleşmiyor.");
            }

            if (imageObject.WriterImage == null)
            {
                ModelState.AddModelError("WriterImage", "Lütfen bir profil resmi yükleyiniz.");
            }

            Writer writer = new();
            WriterValidator writerValidator = new();

            var extension = Path.GetExtension(imageObject.WriterImage?.FileName);
            var newImageName = Guid.NewGuid() + extension;

            writer.WriterImage = "/WriterImageFiles/" + newImageName;
            writer.WriterName = imageObject.WriterName;
            writer.WriterMail = imageObject.WriterMail;
            writer.WriterPassword = imageObject.WriterPassword;
            writer.WriterAbout = imageObject.WriterAbout;
            writer.WriterStatus = true;

            var result = writerValidator.Validate(writer);

            if (result.IsValid && imageObject.WriterImage != null)
            {
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/" + newImageName);
                var stream = new FileStream(location, FileMode.Create);
                imageObject.WriterImage.CopyTo(stream);

                writerManager.AddEntity(writer);
                return RedirectToAction("Index", "Dashboard");
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

        #endregion
    }
}
