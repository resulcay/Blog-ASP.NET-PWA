using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class CommentManager : ICommentService
    {
        readonly ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public void AddEntity(Comment entity)
        {
            _commentDal.Insert(entity);
        }

        public void DeleteEntity(Comment entity)
        {
            _commentDal.Delete(entity);
        }

        public void UpdateEntity(Comment entity)
        {
            _commentDal.Update(entity);
        }

        public Comment GetEntityById(int id)
        {
            return _commentDal.GetById(id);
        }

        public List<Comment> GetEntities()
        {
            return _commentDal.ListAll();
        }

        public List<Comment> GetCommentsByBlogId(int id)
        {
            return _commentDal.ListAll(x => x.BlogID == id);
        }

        public List<Comment> GetCommentsWithBlogAndWriter()
        {
            return _commentDal.GetCommentsWithBlogAndWriter();
        }
    }
}
