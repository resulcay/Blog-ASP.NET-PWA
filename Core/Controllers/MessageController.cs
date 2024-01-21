using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Core.Controllers
{
	public class MessageController : Controller
	{
		readonly Message2Manager manager = new(new EfMessage2Repository());

		[AllowAnonymous]
		public IActionResult Inbox()
		{
			int writerID = 1;
			var values = manager.GetMessagesByWriterName(writerID);

			return View(values);
		}

		[AllowAnonymous]
		public IActionResult DeleteMessage(int id)
		{
			Message2 message2 = manager.GetEntityById(id);
			manager.DeleteEntity(message2);

			return RedirectToAction("Inbox", "Message");
		}

		[AllowAnonymous]
		[HttpGet]
		public IActionResult MessageAdd()
		{
			return View();
		}

		// TODO: MessageAdd as Post
		//[AllowAnonymous]
		//[HttpPost]
		//public IActionResult MessageAdd(Message2 message2)
		//{
		//    return View();
		//}
	}
}
