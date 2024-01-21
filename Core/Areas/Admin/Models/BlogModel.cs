using System;

namespace Core.Areas.Admin.Models
{
	public class BlogModel
	{
		public int ID { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }

		public DateTime CreateDate { get; set; }

		public string Category { get; set; }

		public string Writer { get; set; }
	}
}
