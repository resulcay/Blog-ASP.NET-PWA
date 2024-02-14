using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IWriterDal : IGenericDal<Writer>
    {
        Writer GetWriterBySession(string session);
    }
}
