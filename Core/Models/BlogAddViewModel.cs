using Microsoft.AspNetCore.Http;
using System;

namespace Core.Models
{
    public class BlogAddViewModel
    {
        public string BlogTitle { get; set; }

        public string BlogContent { get; set; }

        public IFormFile BlogImage { get; set; }

        public int CategoryID { get; set; }
    }
}
