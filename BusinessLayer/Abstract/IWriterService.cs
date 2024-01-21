﻿using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
	public interface IWriterService : IGenericService<Writer>
	{
		int GetWriterIDBySession(string session);
	}
}
