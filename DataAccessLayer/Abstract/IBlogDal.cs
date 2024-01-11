using EntityLayer.Concrete;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract
{
    public interface IBlogDal : IGenericDal<Blog>
    {
        List<Blog> GetLastBlogs();
        List<Blog> GetListWithCategory(int? id);
        List<Blog> GetBlogListByWriter(int id);
    }
}
