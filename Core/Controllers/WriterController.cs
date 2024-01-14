using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using Core.Models;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace Core.Controllers
{
    public class WriterController : Controller
    {
        readonly WriterManager writerManager = new(new EfWriterRepository());

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WriterProfile()
        {
            return View();
        }

        [AllowAnonymous]
        public PartialViewResult WriterNavBarPartial()
        {
            return PartialView();
        }

        [AllowAnonymous]
        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult WriterEditProfile()
        {
            var values = writerManager.GetEntityById(1);
            return View(values);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult WriterEditProfile(Writer writer, string confirmPassword)
        {
            if (writer.WriterPassword != confirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Şifreler eşleşmiyor.");
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

            writer.WriterImage = newImageName;
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
    }
}
