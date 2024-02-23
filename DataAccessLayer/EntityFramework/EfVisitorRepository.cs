using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System.Linq;

namespace DataAccessLayer.EntityFramework
{
    public class EfVisitorRepository : GenericRepository<Visitor>, IVisitorDal
    {
        public bool IsVisitorUnique(string ip)
        {
            using var context = new Context();
            var visitor = context.Visitors.Where(a => a.VisitorIp == ip).FirstOrDefault();

            if (visitor == null)
            {
                return true;
            }

            return false;

        }
    }
}
