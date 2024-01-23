using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        void Insert(T item);
        void Delete(T item);
        void Update(T item);
        List<T> ListAll();
        T GetById(int id);
        List<T> ListAll(Expression<Func<T, bool>> filter);
    }
}

