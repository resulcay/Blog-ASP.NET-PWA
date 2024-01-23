using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void AddEntity(Category entity)
        {
            _categoryDal.Insert(entity);
        }

        public void UpdateEntity(Category entity)
        {
            _categoryDal.Update(entity);
        }

        public void DeleteEntity(Category entity)
        {
            _categoryDal.Delete(entity);
        }

        public List<Category> GetEntities()
        {
            return _categoryDal.ListAll();
        }

        public Category GetEntityById(int id)
        {
            return _categoryDal.GetById(id);
        }

        public List<Category> GetCategoryListWithBlogCount()
        {
            return _categoryDal.GetCategoryListWithBlogCount();
        }
    }
}
