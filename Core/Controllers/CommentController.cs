using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class CommentController : Controller
    {
        private readonly CommentManager _commentManager = new(new EfCommentRepository());
        private readonly WriterManager _writerManager = new(new EfWriterRepository());
        private readonly UserManager<User> _userManager;

        public CommentController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public PartialViewResult PartialAddComment()
        {
            return PartialView();
        }

        [AllowAnonymous]
        [HttpPost]
        public PartialViewResult PartialAddComment(Comment comment)
        {
            string image;
            var writer = GetWriterID().Result;

            if (writer != null)
            {
                image = writer.WriterImage;
            }
            else
            {
                image = "/coredemotheme/images/default.png";
            }

            comment.CommentStatus = true;
            comment.Image = image;
            comment.CommentCreatedAt = DateTime.Parse(DateTime.Now.ToShortDateString());

            _commentManager.AddEntity(comment);

            return PartialView();
        }

        [AllowAnonymous]
        public PartialViewResult CommentListByBlog(int id)
        {
            var values = _commentManager.GetCommentsByBlogId(id);
            return PartialView(values);
        }

        private async Task<Writer> GetWriterID()
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                string userId = await _userManager.GetUserIdAsync(user);
                var writer = _writerManager.GetWriterBySession(userId);

                return writer;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
