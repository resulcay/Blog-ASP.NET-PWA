using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}
