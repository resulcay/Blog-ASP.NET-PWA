using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class BlogManager : IBlogService
    {
        readonly IBlogDal _blogDal;

        public BlogManager(IBlogDal blogDal)
        {
            _blogDal = blogDal;
        }

        public void AddEntity(Blog entity)
        {
            _blogDal.Insert(entity);
        }

        public void DeleteEntity(Blog entity)
        {
            _blogDal.Delete(entity);
        }

        public void UpdateEntity(Blog entity)
        {
            _blogDal.Update(entity);
        }

        public Blog GetEntityById(int id)
        {
            return _blogDal.GetById(id);
        }

        public List<Blog> GetEntities()
        {
            return _blogDal.ListAll();
        }

        public List<Blog> GetBlogListWithCategory(int? length)
        {
            return _blogDal.GetListWithCategory(length);
        }

        public List<Blog> GetBlogListByWriter(int id, bool isWriter)
        {
            return _blogDal.GetBlogListByWriter(id, isWriter);
        }

        public List<Blog> GetLastBlogs()
        {
            return _blogDal.GetLastBlogs();
        }

        public int TotalBlogCountByWriter(int id)
        {
            return _blogDal.TotalBlogCountByWriter(id);
        }

        public List<Blog> GetDetailedBlogList()
        {
            return _blogDal.GetDetailedBlogList();
        }

        public Blog GetBlogWithCommentCount(int id)
        {
            return _blogDal.GetBlogWithCommentCount(id);
        }
    }
}