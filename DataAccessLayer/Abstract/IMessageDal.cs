using EntityLayer.Concrete;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract
{
    public interface IMessageDal : IGenericDal<Message>
    {
        List<Message> GetReceivedMessagesByWriter(int id);
        List<Message> GetSentMessagesByWriter(int id);
        List<Message> GetDetailedMessages();
    }
}
