using System;
using System.Collections.Generic;

namespace Application.Repositories
{
	public interface IRepository<TEntity> where TEntity : class
	{
		TEntity Select(object id);
		
		IEnumerable<TEntity> Select();
		
		void Insert(TEntity entity);
		
		void Update(TEntity entity);
		
		void Delete(TEntity entity);
	}
}
