using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IMessageService : IGenericService<Message>
    {
        List<Message> GetReceivedMessagesByWriter(int id);
        List<Message> GetSentMessagesByWriter(int id);
        List<Message> GetDetailedMessages();
    }
}
