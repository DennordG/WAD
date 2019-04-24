using System.Linq;
using System.Threading.Tasks;
using WAD_Application.Models;
using WAD_Application.Services.Interfaces;

namespace WAD_Application.Services
{
	public class UserService : AbstractService<User>
	{
		public UserService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

		public override async Task AddAsync(User user)
		{
			_unitOfWork.Users.Add(user);
			await _unitOfWork.SaveChangesAsync();
		}

		public override IQueryable<User> All()
		{
			return _unitOfWork.Users.All();
		}

		public override async Task DeleteAsync(User user)
		{
			_unitOfWork.Users.Delete(user);
			await _unitOfWork.SaveChangesAsync();
		}

		public override async Task<User> FindByIdAsync(int? id)
		{
			if (id.HasValue)
			{
				return await _unitOfWork.Users.FindByIdAsync(id.Value);
			}
			else
			{
				return null;
			}
		}

		public override async Task UpdateAsync(User user)
		{
			_unitOfWork.Users.Update(user);
			await _unitOfWork.SaveChangesAsync();
		}
	}
}
