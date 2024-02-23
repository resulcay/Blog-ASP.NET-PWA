using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IVisitorDal : IGenericDal<Visitor>
    {
        bool IsVisitorUnique(string ip);
    }
}
