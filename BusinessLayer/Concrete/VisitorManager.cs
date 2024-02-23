using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class VisitorManager : IVisitorService
    {
        readonly IVisitorDal _visitorDal;

        public VisitorManager(IVisitorDal visitorDal)
        {
            _visitorDal = visitorDal;
        }

        public void AddEntity(Visitor entity)
        {
            _visitorDal.Insert(entity);
        }

        public void UpdateEntity(Visitor entity)
        {
            _visitorDal.Update(entity);
        }

        public void DeleteEntity(Visitor entity)
        {
            _visitorDal.Delete(entity);
        }

        public List<Visitor> GetEntities()
        {
            return _visitorDal.ListAll();
        }

        public Visitor GetEntityById(int id)
        {
            return _visitorDal.GetById(id);
        }

        public bool IsVisitorUnique(string ip)
        {
            return _visitorDal.IsVisitorUnique(ip);
        }
    }
}
