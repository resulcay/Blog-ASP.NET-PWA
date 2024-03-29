﻿using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class WriterManager : IWriterService
    {
        private readonly IWriterDal _writerDal;

        public WriterManager(IWriterDal writerDal)
        {
            _writerDal = writerDal;
        }

        public void AddEntity(Writer entity)
        {
            _writerDal.Insert(entity);
        }

        public void UpdateEntity(Writer entity)
        {
            _writerDal.Update(entity);
        }

        public void DeleteEntity(Writer entity)
        {
            _writerDal.Delete(entity);
        }

        public List<Writer> GetEntities()
        {
            return _writerDal.ListAll();
        }

        public Writer GetEntityById(int id)
        {
            return _writerDal.GetById(id);
        }

        public Writer GetWriterBySession(string session)
        {
            return _writerDal.GetWriterBySession(session);
        }
    }
}
