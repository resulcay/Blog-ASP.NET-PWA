using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Core.ViewComponents.Writer
{
    public class WriterAboutOnDashboard : ViewComponent
    {
        readonly WriterManager manager = new(new EfWriterRepository());

        public IViewComponentResult Invoke()
        {
            var userMail = User.Identity.Name;
            var writerID = manager.GetWriterIDBySession(userMail);
            var writer = manager.GetEntityById(writerID);

            return View(writer);
        }
    }
}
