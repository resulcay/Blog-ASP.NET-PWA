using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IVisitorService : IGenericService<Visitor>
    {
        bool IsVisitorUnique(string ip);
    }
}