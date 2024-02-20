using EntityLayer.Concrete;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract
{
    public interface IBlogDal : IGenericDal<Blog>
    {
        int TotalBlogCountByWriter(int id);
        Blog GetBlogWithCommentCount(int id);
        List<Blog> GetLastBlogs();
        List<Blog> GetListWithCategory(int? id);
        List<Blog> GetBlogListByWriter(int id, bool isWriter);
        List<Blog> GetDetailedBlogList();
    }
}
