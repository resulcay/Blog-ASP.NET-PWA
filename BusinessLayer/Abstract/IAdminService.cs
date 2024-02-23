using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAdminService
    {
        List<int> SmallQueryData(int id);
        List<int> ComplexQueryData();
        List<string> GetRoles();
    }
}
