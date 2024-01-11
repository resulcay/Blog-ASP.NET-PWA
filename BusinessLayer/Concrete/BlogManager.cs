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

        public List<Blog> GetBlogListByWriter(int id)
        {
            return _blogDal.GetBlogListByWriter(id);
        }

        public List<Blog> GetLastBlogs()
        {
            return _blogDal.GetLastBlogs();
        }
    }
}