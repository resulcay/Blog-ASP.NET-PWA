using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Core.ViewComponents.Writer
{
	public class WriterNotification : ViewComponent
	{
		readonly NotificationManager notificationManager = new(new EfNotificationRepository());

		public IViewComponentResult Invoke()
		{
			var values = notificationManager.GetEntities();
			return View(values);
		}
	}
}
