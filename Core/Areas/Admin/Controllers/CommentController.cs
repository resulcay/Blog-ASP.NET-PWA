using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
        readonly CommentManager manager = new(new EfCommentRepository());

        public IActionResult Index()
        {
            var comments = manager.GetCommentsWithBlogAndWriter();
            return View(comments);
        }

        public IActionResult DeleteComment(int id)
        {
            Comment comment = manager.GetEntityById(id);
            manager.DeleteEntity(comment);

            return RedirectToAction("Index", "Comment");
        }
    }
}
