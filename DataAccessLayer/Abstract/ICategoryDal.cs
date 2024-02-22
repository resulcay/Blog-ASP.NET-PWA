using EntityLayer.Concrete;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract
{
    public interface ICategoryDal : IGenericDal<Category>
    {
        List<Category> GetCategoryListWithBlogCount();
        Dictionary<string, int> GetCategoryWithBlogCount();
    }
}
