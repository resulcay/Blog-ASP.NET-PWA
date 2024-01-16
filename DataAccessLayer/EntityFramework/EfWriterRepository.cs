using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System.Linq;

namespace DataAccessLayer.EntityFramework
{
    public class EfWriterRepository : GenericRepository<Writer>, IWriterDal
    {
        public int GetWriterIDBySession(string session)
        {
            Context context = new();
            var writerID = context.Writers.Where(x => x.WriterMail == session).Select(y => y.WriterID).FirstOrDefault();

            return writerID;
        }
    }
}
