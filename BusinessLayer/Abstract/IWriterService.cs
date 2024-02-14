using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IWriterService : IGenericService<Writer>
    {
        Writer GetWriterBySession(string session);
    }
}
