using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class VisitorController : Controller
    {
        private readonly VisitorManager _visitorManager = new( new EfVisitorRepository());

        public IActionResult Index()
        {
            var visitors = _visitorManager.GetEntities();
            return View(visitors);
        }
    }
}
