using System.Collections.Generic;

namespace DataAccessLayer.Abstract
{
    public interface IAdminDal
    {
        List<int> SmallQueryData (int id);
        List<int> ComplexQueryData();
        List<string> GetRoles();
    }
}
