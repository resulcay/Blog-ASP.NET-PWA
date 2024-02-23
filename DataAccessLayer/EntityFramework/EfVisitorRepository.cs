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
            return context.Visitors.Any(x => x.VisitorIp != ip);
        }
    }
}
