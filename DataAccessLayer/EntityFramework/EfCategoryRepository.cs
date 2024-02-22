using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.EntityFramework
{
    public class EfCategoryRepository : GenericRepository<Category>, ICategoryDal
    {
        public List<Category> GetCategoryListWithBlogCount()
        {
            using var context = new Context();
            var categoriesWithBlogCounts = context.Categories.Include(x => x.Blogs.Where(y => y.BlogStatus))
                .Where(c => c.Blogs.Any(x => x.BlogStatus) && c.CategoryStatus)
                .OrderByDescending(c => c.Blogs.Count)
                .Take(7)
                .ToList();

            return categoriesWithBlogCounts;
        }

        public Dictionary<string, int> GetCategoryWithBlogCount()
        {
            using var context = new Context();
            var categoriesWithBlogCounts = context.Categories.Include(x => x.Blogs)
                .ToDictionary(c => c.CategoryName, c => c.Blogs.Count);

            return categoriesWithBlogCounts;
        }
    }
}
