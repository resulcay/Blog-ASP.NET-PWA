﻿using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface ICommentService
	{
		void CommentAdd(Comment comment);
		void CommentDelete(Comment comment);
		void CommentUpdate(Comment comment);
		Comment GetCommentById(int id);
		List<Comment> GetComments(int id);
	}
}
