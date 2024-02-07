using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IContactService
    {
        void ContactAdd(Contact contact);
        List<Contact> GetAll();
        Contact GetEntityById(int id);
    }
}
