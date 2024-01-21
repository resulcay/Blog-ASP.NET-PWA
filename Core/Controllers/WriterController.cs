using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Controllers
{
	public class WriterController : Controller
	{
		readonly WriterManager writerManager = new(new EfWriterRepository());
		readonly Context context = new();

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
			var userName = User.Identity.Name;
			var userMail = context.Users.Where(x => x.UserName == userName).Select(x => x.Email).FirstOrDefault();
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
