using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class NotificationManager : INotificationService
    {
        readonly INotificationDal _notificationDal;

        public NotificationManager(INotificationDal notificationDal)
        {
            _notificationDal = notificationDal;
        }

        public void AddEntity(Notification entity)
        {
            _notificationDal.Insert(entity);
        }

        public void UpdateEntity(Notification entity)
        {
            _notificationDal.Update(entity);
        }

        public void DeleteEntity(Notification entity)
        {
            _notificationDal.Delete(entity);
        }

        public List<Notification> GetEntities()
        {
            return _notificationDal.ListAll();
        }

        public Notification GetEntityById(int id)
        {
            return _notificationDal.GetById(id);
        }
    }
}
