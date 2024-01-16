using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IMessage2Service : IGenericService<Message2>
    {
        List<Message2> GetMessagesByWriter(int id);
        List<Message2> GetMessagesByWriterName(int id);
    }
}
