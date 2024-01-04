using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CoreDemo.Controllers
{
	public class CommentController : Controller
	{
		readonly CommentManager manager = new (new EfCommentRepository());

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public PartialViewResult PartialAddComment() 
		{
			return PartialView();
		}

		[HttpPost]
		public PartialViewResult PartialAddComment(Comment comment)
		{
			comment.CommentStatus = true;
			comment.BlogID = 10;
			comment.CommentCreatedAt = DateTime.Parse(DateTime.Now.ToShortDateString());

			manager.CommentAdd(comment);

			return PartialView();
		}
		public PartialViewResult CommentListByBlog(int id)
		{
			var values = manager.GetComments(id);
			return PartialView(values);
		}
	}
}
