using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WriterController : Controller
    {
        private readonly WriterManager _writerManager = new(new EfWriterRepository());

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetWriterByID(int writerId)
        {
            var writer = _writerManager.GetEntityById(writerId);
            var jsonWriter = JsonConvert.SerializeObject(writer);

            return Json(jsonWriter);
        }

        public IActionResult WriterList()
        {
            var writers = _writerManager.GetEntities();
            var jsonWriters = JsonConvert.SerializeObject(writers);

            return Json(jsonWriters);
        }
    }
}