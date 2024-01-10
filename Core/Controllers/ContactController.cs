using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Core.Controllers
{
    public class ContactController : Controller
    {
        readonly ContactManager _contactManager = new(new EfContactRepository());

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Contact contact)
        {
            contact.ContactCreatedAt = DateTime.Parse(DateTime.Now.ToShortDateString());
            contact.ContactStatus = true;
            _contactManager.ContactAdd(contact);
            return RedirectToAction("Index", "Blog");
        }
    }
}
