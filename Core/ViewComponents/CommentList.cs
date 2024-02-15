using CoreDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CoreDemo.ViewComponents
{
    public class CommentList : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var comments = new List<UserComment>
            {
                new() {
                    ID = 1,
                    Username = "Resul",
                },
                new() {
                    ID = 2,
                    Username = "Hayri",
                },
                new() {
                    ID = 3,
                    Username = "Sefa",
                },
            };
            return View(comments);
        }
    }
}
