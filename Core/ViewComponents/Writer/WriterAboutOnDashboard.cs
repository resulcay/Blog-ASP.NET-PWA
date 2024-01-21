using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Core.ViewComponents.Writer
{
	public class WriterAboutOnDashboard : ViewComponent
	{
		readonly WriterManager _manager = new(new EfWriterRepository());
		readonly Context _context = new();

		public IViewComponentResult Invoke()
		{
			var userName = User.Identity.Name;
			var userMail = _context.Users.Where(x => x.UserName == userName).Select(x => x.Email).FirstOrDefault();
			var writerID = _manager.GetWriterIDBySession(userMail);
			var writer = _manager.GetEntityById(writerID);

			return View(writer);
		}
	}
}
