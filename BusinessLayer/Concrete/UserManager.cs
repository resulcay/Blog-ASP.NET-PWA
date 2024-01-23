using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void AddEntity(AppUser entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(AppUser entity)
        {
            throw new NotImplementedException();
        }

        public List<AppUser> GetEntities()
        {
            throw new NotImplementedException();
        }

        public AppUser GetEntityById(int id)
        {
            return _userDal.GetById(id);
        }

        public void UpdateEntity(AppUser entity)
        {
            _userDal.Update(entity);
        }
    }
}
