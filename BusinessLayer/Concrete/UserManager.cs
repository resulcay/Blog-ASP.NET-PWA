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

        public void AddEntity(User entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(User entity)
        {
            throw new NotImplementedException();
        }

        public List<User> GetEntities()
        {
            throw new NotImplementedException();
        }

        public User GetEntityById(int id)
        {
            return _userDal.GetById(id);
        }

        public void UpdateEntity(User entity)
        {
            _userDal.Update(entity);
        }
    }
}
