using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IBlogService : IGenericService<Blog>
    {
        List<Blog> GetLastBlogs();
        List<Blog> GetBlogListWithCategory(int? length);
        List<Blog> GetBlogListByWriter(int id);
    }
}