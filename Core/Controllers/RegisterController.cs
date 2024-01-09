using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace CoreDemo.Controllers
{
	public class RegisterController : Controller
	{
		readonly WriterManager manager = new(new EfWriterRepository());
		readonly List<string> cities = new()
			{
			"Adana", "Adıyaman", "Afyon", "Ağrı", "Amasya", "Ankara", "Antalya", "Artvin",
			"Aydın", "Balıkesir", "Bilecik", "Bingöl", "Bitlis", "Bolu", "Burdur", "Bursa", "Çanakkale",
			"Çankırı", "Çorum", "Denizli", "Diyarbakır", "Edirne", "Elazığ", "Erzincan", "Erzurum", "Eskişehir",
			"Gaziantep", "Giresun", "Gümüşhane", "Hakkari", "Hatay", "Isparta", "Mersin", "İstanbul", "İzmir",
			"Kars", "Kastamonu", "Kayseri", "Kırklareli", "Kırşehir", "Kocaeli", "Konya", "Kütahya", "Malatya",
			"Manisa", "Kahramanmaraş", "Mardin", "Muğla", "Muş", "Nevşehir", "Niğde", "Ordu", "Rize", "Sakarya",
			"Samsun", "Siirt", "Sinop", "Sivas", "Tekirdağ", "Tokat", "Trabzon", "Tunceli", "Şanlıurfa", "Uşak",
			"Van", "Yozgat", "Zonguldak", "Aksaray", "Bayburt", "Karaman", "Kırıkkale", "Batman", "Şırnak",
			"Bartın", "Ardahan", "Iğdır", "Yalova", "Karabük", "Kilis", "Osmaniye", "Düzce",
			};

		[HttpGet]

		public IActionResult Index()
		{
			RegisterViewModel model = new()
			{
				Cities = cities,
				Writer = new Writer()
			};

			return View(model);
		}

		[HttpPost]
		public IActionResult Index(Writer writer)
		{
			RegisterViewModel model = new()
			{
				Cities = cities,
				Writer = writer
			};

			WriterValidator writerValidator = new();
			ValidationResult result = writerValidator.Validate(writer);


			if (result.IsValid)
            {
				writer.WriterStatus = true;
				writer.WriterAbout = "Test";
				manager.WriterAdd(writer);

				return RedirectToAction("Index", "Blog");
			}
            else
            {
                foreach (var item in result.Errors)
				{ 
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
            }

			return View(model);
		}
	}

	public class RegisterViewModel
	{
		public List<string> Cities { get; set; }
		public Writer Writer { get; set; }
	}
}
