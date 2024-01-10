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
        public List<Blog> GetListWithCategory()
        {
            using var context = new Context();
            return context.Blogs.Include(c => c.Category).ToList();
        }

        public List<Blog> GetBlogListByWriter(int id)
        {
            using var context = new Context();
            return context.Blogs.Include(c => c.Category).Where(w => w.WriterID == id).ToList();
        }
    }
}
