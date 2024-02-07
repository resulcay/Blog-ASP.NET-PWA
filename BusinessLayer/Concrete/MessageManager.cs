using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
        readonly IMessageDal _message2Dal;

        public MessageManager(IMessageDal message2Dal)
        {
            _message2Dal = message2Dal;
        }

        public void AddEntity(Message entity)
        {
            _message2Dal.Insert(entity);
        }

        public void UpdateEntity(Message entity)
        {
            _message2Dal.Update(entity);
        }

        public void DeleteEntity(Message entity)
        {
            _message2Dal.Delete(entity);
        }

        public List<Message> GetEntities()
        {
            return _message2Dal.ListAll();
        }

        public Message GetEntityById(int id)
        {
            return _message2Dal.GetById(id);
        }

        public List<Message> GetReceivedMessagesByWriter(int id)
        {
            return _message2Dal.GetReceivedMessagesByWriter(id);
        }

        public List<Message> GetSentMessagesByWriter(int id)
        {
            return _message2Dal.GetSentMessagesByWriter(id);
        }

        public List<Message> GetDetailedMessages()
        {
            return _message2Dal.GetDetailedMessages();
        }
    }
}
