using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class CommentManager : ICommentService
	{
		readonly ICommentDal _commentDal;

		public CommentManager(ICommentDal commentDal)
		{
			_commentDal = commentDal;
		}

		public void CommentAdd(Comment comment)
		{
			_commentDal.Insert(comment);
		}

		public void CommentDelete(Comment comment)
		{
			_commentDal.Delete(comment);
		}

		public void CommentUpdate(Comment comment)
		{
			_commentDal.Update(comment);
		}

		public Comment GetCommentById(int id)
		{
			return _commentDal.GetById(id);
		}

		public List<Comment> GetComments(int id)
		{
			return _commentDal.ListAll(x => x.BlogID == id);
		}
	}
}
