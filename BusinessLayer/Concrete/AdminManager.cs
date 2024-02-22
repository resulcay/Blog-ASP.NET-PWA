using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class AdminManager : IAdminService
    {
        readonly IAdminDal _adminDal;

        public AdminManager(IAdminDal adminDal)
        {
            _adminDal = adminDal;
        }

        public List<int> ComplexQueryData()
        {
            return _adminDal.ComplexQueryData();
        }

        public List<int> SmallQueryData(int id)
        {
            return _adminDal.SmallQueryData(id);
        }
    }
}
