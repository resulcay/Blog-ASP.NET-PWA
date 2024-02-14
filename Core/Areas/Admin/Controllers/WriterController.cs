using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using Core.Areas.Admin.Models;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WriterController : Controller
    {
        readonly WriterManager writerManager = new(new EfWriterRepository());

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetWriterByID(int writerId)
        {
            var writer = writers.FirstOrDefault(x => x.ID == writerId);
            var jsonWriter = JsonConvert.SerializeObject(writer);

            return Json(jsonWriter);
        }

        public IActionResult WriterList()
        {
            var jsonWriters = JsonConvert.SerializeObject(writers);
            return Json(jsonWriters);
        }

        private static readonly List<WriterModel> writers = new()
        {
            new WriterModel {ID = 1, Name = "J. K. Rowling"},
            new WriterModel {ID = 2, Name = "Stephen King"},
            new WriterModel {ID = 3, Name = "George R. R. Martin"},
            new WriterModel {ID = 4, Name = "J. R. R. Tolkien"},
            new WriterModel {ID = 5, Name = "Dan Brown"},
            new WriterModel {ID = 6, Name = "Agatha Christie"},
            new WriterModel {ID = 7, Name = "Terry Pratchett"},
        };

        [HttpGet]
        public IActionResult WriterAdd()
        {
            return View();
        }

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
            writer.WriterNameSurname = imageObject.WriterName;
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
                return RedirectToAction("Index", "Writer");
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
