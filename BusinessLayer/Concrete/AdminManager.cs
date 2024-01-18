using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AdminManager : IAdminService
    {
        readonly IAdminDal _adminDal;

        public AdminManager(IAdminDal adminDal)
        {
            _adminDal = adminDal;
        }

        public void AddEntity(Admin entity)
        {
           _adminDal.Insert(entity);
        }

        public void UpdateEntity(Admin entity)
        {
           _adminDal.Update(entity);
        }

        public void DeleteEntity(Admin entity)
        {
            _adminDal.Delete(entity);
        }

        public List<Admin> GetEntities()
        {
           return _adminDal.ListAll();
        }

        public Admin GetEntityById(int id)
        {
            return _adminDal.GetById(id);
        }
    }
}
