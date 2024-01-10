using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        public void Delete(T item)
        {
            using var context = new Context();
            context.Remove(item);
            context.SaveChanges();
        }

        public List<T> ListAll()
        {
            using var context = new Context();
            return context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            using var context = new Context();
            return context.Set<T>().Find(id);
        }

        public void Insert(T item)
        {
            using var context = new Context();
            context.Add(item);
            context.SaveChanges();
        }

        public List<T> ListAll(Expression<Func<T, bool>> filter)
        {
            using var context = new Context();
            return context.Set<T>().Where(filter).ToList();
        }

        public void Update(T item)
        {
            using var context = new Context();
            context.Update(item);
            context.SaveChanges();
        }
    }
}
