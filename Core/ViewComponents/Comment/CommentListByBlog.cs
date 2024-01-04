using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.Comment
{
    public class CommentListByBlog : ViewComponent
    {
        readonly CommentManager manager = new(new EfCommentRepository());

        public IViewComponentResult Invoke(int id) 
        {
            ViewBag.i = id;
            var values = manager.GetComments(id);
            return View(values);
        }
    }
}
