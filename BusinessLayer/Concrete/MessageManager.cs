using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
        readonly IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public void AddEntity(Message entity)
        {
            _messageDal.Insert(entity);
        }

        public void UpdateEntity(Message entity)
        {
            _messageDal.Update(entity);
        }

        public void DeleteEntity(Message entity)
        {
            _messageDal.Delete(entity);
        }

        public List<Message> GetEntities()
        {
            return _messageDal.ListAll();
        }

        public Message GetEntityById(int id)
        {
            return _messageDal.GetById(id);
        }

        public List<Message> GetReceivedMessagesByWriter(int id)
        {
            return _messageDal.GetReceivedMessagesByWriter(id);
        }

        public List<Message> GetSentMessagesByWriter(int id)
        {
            return _messageDal.GetSentMessagesByWriter(id);
        }

        public List<Message> GetDetailedMessages()
        {
            return _messageDal.GetDetailedMessages();
        }
    }
}
