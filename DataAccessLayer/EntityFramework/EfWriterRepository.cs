using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccessLayer.EntityFramework
{
    public class EfWriterRepository : GenericRepository<Writer>, IWriterDal
    {
        public Writer GetWriterBySession(string session)
        {
            Context context = new();
            var writer = context.Writers.Include(t => t.User).Where(x => x.UserID == int.Parse(session) && x.WriterStatus).FirstOrDefault();

            return writer;
        }
    }
}
