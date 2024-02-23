using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IAdminService
    {
        List<int> SmallQueryData(int id);
        List<int> ComplexQueryData();
        List<string> GetRoles();
    }
}
