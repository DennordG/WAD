using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WAD_Application.Data;
using WAD_Application.Repositories.Interfaces;

namespace WAD_Application.Repositories
{
	public class Repository<T> : IRepository<T>
		where T : class
	{
		private ApplicationDbContext _context;
		private DbSet<T> _dbSet;

		public Repository(ApplicationDbContext context)
		{
			_context = context;
			_dbSet = context.Set<T>();
		}

		public virtual IQueryable<T> All()
		{
			return _dbSet;
		}

		public virtual IEnumerable<T> Get(
			Expression<Func<T, bool>> filter = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			string includeProperties = "")
		{
			IQueryable<T> query = _dbSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			foreach (var includeProperty in includeProperties.Split
				(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			if (orderBy != null)
			{
				return orderBy(query).ToList();
			}
			else
			{
				return query.ToList();
			}
		}

		public virtual void Add(T entity)
		{
			_dbSet.Add(entity);
		}

		public virtual void Delete(T entity)
		{
			if (_context.Entry(entity).State == EntityState.Detached)
			{
				_dbSet.Attach(entity);
			}

			_dbSet.Remove(entity);
		}

		public virtual void DeleteById(int id)
		{
			T entity = _dbSet.Find(id);
			Delete(entity);
		}

		public virtual async Task<T> FindByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		public virtual void Update(T entity)
		{
			_dbSet.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
		}
	}
}
