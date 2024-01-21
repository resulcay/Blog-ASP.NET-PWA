using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
	public interface IGenericService<T>
	{
		void AddEntity(T entity);
		void DeleteEntity(T entity);
		void UpdateEntity(T entity);
		T GetEntityById(int id);
		List<T> GetEntities();
	}
}
