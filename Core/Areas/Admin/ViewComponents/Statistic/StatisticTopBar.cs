using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Core.Areas.Admin.ViewComponents.Statistic
{
    public class StatisticTopBar : ViewComponent
    {
        readonly BlogManager blogManager = new(new EfBlogRepository());
        readonly Message2Manager message2Manager = new(new EfMessage2Repository());
        readonly CommentManager commentManager = new(new EfCommentRepository());

        public IViewComponentResult Invoke()
        {
            // TODO: will impact performance if there are many.
            int blogCount = blogManager.GetEntities().Count;
            int messageCount = message2Manager.GetEntities().Count;
            int commentCount = commentManager.GetEntities().Count;

            ViewBag.blogCount = blogCount;
            ViewBag.messageCount = messageCount;
            ViewBag.commentCount = commentCount;

            return View();
        }
    }
}