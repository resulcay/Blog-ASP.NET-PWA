using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IBlogService : IGenericService<Blog>
    {
        int TotalBlogCountByWriter(int id);
        Blog GetBlogWithCommentCount(int id);
        List<Blog> GetLastBlogs();
        List<Blog> GetBlogListWithCategory(int? length);
        List<Blog> GetBlogListByWriter(int id, bool isWriter);
        List<Blog> GetDetailedBlogList();
    }
}