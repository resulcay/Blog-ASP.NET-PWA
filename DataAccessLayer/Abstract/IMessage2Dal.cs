﻿using EntityLayer.Concrete;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract
{
    public interface IMessage2Dal : IGenericDal<Message2>
    {
        List<Message2> GetReceivedMessagesByWriter(int id);
        List<Message2> GetSentMessagesByWriter(int id);
        List<Message2> GetDetailedMessages();
    }
}
