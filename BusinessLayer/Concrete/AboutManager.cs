using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
	public class AboutManager : IAboutService
	{
		readonly IAboutDal _aboutDal;

		public AboutManager(IAboutDal aboutDal)
		{
			_aboutDal = aboutDal;
		}

		public void AddEntity(About entity)
		{
			_aboutDal.Insert(entity);
		}

		public void UpdateEntity(About entity)
		{
			_aboutDal.Update(entity);
		}

		public void DeleteEntity(About entity)
		{
			_aboutDal.Delete(entity);
		}

		public List<About> GetEntities()
		{
			return _aboutDal.ListAll();
		}

		public About GetEntityById(int id)
		{
			return _aboutDal.GetById(id);
		}
	}
}