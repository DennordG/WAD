using System.Linq;
using System.Threading.Tasks;
using WAD_Application.Services.Interfaces;

namespace WAD_Application.Services
{
	public abstract class AbstractService<T> : IService<T>
		where T : class
	{
		protected readonly IUnitOfWork _unitOfWork;

		public AbstractService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public virtual Task AddAsync(T entity) => Task.CompletedTask;

		public virtual IQueryable<T> All() => null;

		public virtual Task DeleteAsync(T entity) => Task.CompletedTask;

		public virtual Task<T> FindByIdAsync(int? id) => Task.FromResult<T>(null);

		public virtual Task UpdateAsync(T entity) => Task.CompletedTask;
	}
}
