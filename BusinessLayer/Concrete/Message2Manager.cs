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

        public List<Message2> GetReceivedMessagesByWriter(int id)
        {
            return _message2Dal.GetReceivedMessagesByWriter(id);
        }

        public List<Message2> GetSentMessagesByWriter(int id)
        {
            return _message2Dal.GetSentMessagesByWriter(id);
        }

        public List<Message2> GetDetailedMessages()
        {
            return _message2Dal.GetDetailedMessages();
        }
    }
}
