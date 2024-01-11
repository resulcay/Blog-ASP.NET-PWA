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
        public List<Blog> GetLastBlogs()
        {
            using var context = new Context();

            return context.Blogs.Where(x => x.BlogStatus)
                .OrderByDescending(x => x.BlogCreatedAt)
                .Take(3)
                .ToList();
        }

        public List<Blog> GetListWithCategory(int? length)
        {
            using var context = new Context();
            var query = context.Blogs.Where(x => x.BlogStatus).Include(c => c.Category);

            if (length.HasValue && length > 0)
            {
                return query.OrderByDescending(x => x.BlogCreatedAt).Take((int)length).ToList();
            }

            return query.ToList();
        }

        public List<Blog> GetBlogListByWriter(int id)
        {
            using var context = new Context();
            return context.Blogs.Include(c => c.Category).Where(w => w.WriterID == id && w.BlogStatus).ToList();
        }
    }
}
