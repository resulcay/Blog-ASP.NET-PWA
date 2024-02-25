using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.EntityFramework
{
    public class EfBlogRepository : GenericRepository<Blog>, IBlogDal
    {
        public int TotalBlogCountByWriter(int id)
        {
            using var context = new Context();
            return context.Blogs.Count(x => x.WriterID == id);
        }

        public List<Blog> GetLastBlogs()
        {
            using var context = new Context();

            return context.Blogs.Include(t => t.Category).Include(a => a.Writer).Where(z => z.Category.CategoryStatus && z.Writer.WriterStatus).Where(x => x.BlogStatus)
                .OrderByDescending(x => x.BlogCreatedAt)
                .Take(3)
                .ToList();
        }

        public List<Blog> GetListWithCategory(int? length)
        {
            using var context = new Context();
            var query = context.Blogs.Where(x => x.BlogStatus).Include(c => c.Category).Include(a => a.Writer).Where(t => t.Category.CategoryStatus && t.Writer.WriterStatus);

            if (length.HasValue && length > 0)
            {
                return query.OrderByDescending(x => x.BlogCreatedAt).Take((int)length).ToList();
            }

            return query.ToList();
        }

        public List<Blog> GetBlogListByWriter(int id, bool isWriter)
        {
            using var context = new Context();
            return context.Blogs.Include(c => c.Category).Include(a => a.Writer).Where(g => g.Writer.WriterStatus).Where(w => w.WriterID == id && (isWriter || w.BlogStatus)).ToList();
        }

        public List<Blog> GetDetailedBlogList()
        {
            using var context = new Context();
            return context.Blogs.Include(c => c.Category).Include(a => a.Writer).Where(g => g.Writer.WriterStatus).ToList();
        }

        public Blog GetBlogWithCommentCount(int id)
        {
            using var context = new Context();
            return context.Blogs.Include(c => c.Comments).Include(a => a.Writer).Where(g => g.Writer.WriterStatus).FirstOrDefault(x => x.BlogID == id);
        }
    }
}
