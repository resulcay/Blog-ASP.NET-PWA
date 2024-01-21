using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DateTimeExtensions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Core.ViewComponents.Writer
{
	public class WriterMessageNotification : ViewComponent
	{
		readonly Message2Manager manager = new(new EfMessage2Repository());

		public IViewComponentResult Invoke()
		{
			int writerID = 1;
			var values = manager.GetMessagesByWriterName(writerID);

			foreach (var item in values)
			{
				var now = DateTime.Now;
				var tempDate = item.MessageDate;
				var relativeDate = tempDate.ToNaturalText(now, false);

				item.MessageDetails = relativeDate;
			}

			return View(values);
		}
	}
}
