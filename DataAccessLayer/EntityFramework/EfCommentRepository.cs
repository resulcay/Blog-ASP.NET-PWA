using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.EntityFramework
{
    public class EfCommentRepository : GenericRepository<Comment>, ICommentDal
    {
        public List<Comment> GetCommentsWithBlogAndWriter()
        {
            using var context = new Context();
            return context.Comments.Include(c => c.Blog).Include(d => d.Blog.Writer).Where(a => a.Blog.Writer.WriterStatus && a.CommentStatus).ToList();
        }
    }
}
