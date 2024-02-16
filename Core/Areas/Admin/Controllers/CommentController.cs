using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        private readonly CommentManager _commentManager = new(new EfCommentRepository());

        public IActionResult Index()
        {
            var comments = _commentManager.GetCommentsWithBlogAndWriter();
            return View(comments);
        }

        public IActionResult DeleteComment(int id)
        {
            Comment comment = _commentManager.GetEntityById(id);
            _commentManager.DeleteEntity(comment);

            return RedirectToAction("Index", "Comment");
        }
    }
}
