using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IMessage2Service : IGenericService<Message2>
    {
        List<Message2> GetReceivedMessagesByWriter(int id);
        List<Message2> GetSentMessagesByWriter(int id);
    }
}
