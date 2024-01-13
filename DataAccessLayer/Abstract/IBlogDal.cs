using EntityLayer.Concrete;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract
{
    public interface IBlogDal : IGenericDal<Blog>
    {
        int TotalBlogCount();
        int TotalBlogCountByWriter(int id);
        List<Blog> GetLastBlogs();
        List<Blog> GetListWithCategory(int? id);
        List<Blog> GetBlogListByWriter(int id, bool isWriter);
    }
}
