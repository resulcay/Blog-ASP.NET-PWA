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
            var values = manager.GetEntityById(1);
            return View(values);
        }
    }
}
