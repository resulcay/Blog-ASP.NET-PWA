using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.EntityFramework
{
	public class EfMessage2Repository : GenericRepository<Message2>, IMessage2Dal
	{
		public List<Message2> GetMessagesByWriterName(int id)
		{
			using var context = new Context();
			var messages = context.Message2s.Include(z => z.SenderUser).Where(x => x.ReceiverID == id && x.MessageStatus).ToList();

			return messages;
		}
	}
}
