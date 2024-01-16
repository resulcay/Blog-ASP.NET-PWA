using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class Message2Manager : IMessage2Service
    {
        readonly IMessage2Dal _message2Dal;

        public Message2Manager(IMessage2Dal message2Dal)
        {
            _message2Dal = message2Dal;
        }

        public void AddEntity(Message2 entity)
        {
            _message2Dal.Insert(entity);
        }

        public void UpdateEntity(Message2 entity)
        {
            _message2Dal.Update(entity);
        }

        public void DeleteEntity(Message2 entity)
        {
            _message2Dal.Delete(entity);
        }

        public List<Message2> GetEntities()
        {
            return _message2Dal.ListAll();
        }

        public Message2 GetEntityById(int id)
        {
            return _message2Dal.GetById(id);
        }

        public List<Message2> GetMessagesByWriter(int id)
        {
            return _message2Dal.ListAll(x => x.ReceiverID == id);
        }

        public List<Message2> GetMessagesByWriterName(int id)
        {
            return _message2Dal.GetMessagesByWriterName(id);
        }
    }
}
