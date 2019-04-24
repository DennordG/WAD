using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WAD_Application.Repositories.Interfaces
{
	public interface IRepository<T>
		where T : class
	{
		IQueryable<T> All();
		Task<T> FindByIdAsync(int id);
		void Add(T entity);
		void DeleteById(int id);
		void Delete(T entity);
		void Update(T entity);
		IEnumerable<T> Get(
			Expression<Func<T, bool>> filter,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
			string includeProperties);
	}
}
