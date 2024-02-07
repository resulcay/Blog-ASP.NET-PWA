using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.EntityFramework
{
    public class EfMessageRepository : GenericRepository<Message>, IMessageDal
    {
        public List<Message> GetReceivedMessagesByWriter(int id)
        {
            using var context = new Context();
            var messages = context.Messages.Include(z => z.SenderUser).Include(a => a.ReceiverUser).Where(x => x.ReceiverID == id && x.MessageStatus).ToList();

            return messages;
        }

        public List<Message> GetSentMessagesByWriter(int id)
        {
            using var context = new Context();
            var messages = context.Messages.Include(z => z.SenderUser).Include(a => a.ReceiverUser).Where(x => x.SenderID == id && x.MessageStatus).ToList();

            return messages;
        }

        public List<Message> GetDetailedMessages()
        {
            using var context = new Context();
            var messages = context.Messages.Include(z => z.SenderUser).Include(a => a.ReceiverUser).ToList();

            return messages;
        }
    }
}
