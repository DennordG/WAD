using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WAD_Application.Models;
using WAD_Application.Services.Interfaces;

namespace WAD_Application.Services
{
	public class UserConversationService : AbstractService<UserConversation>
	{
		public UserConversationService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

		public override async Task AddAsync(UserConversation userConversation)
		{
			_unitOfWork.UserConversations.Add(userConversation);
			await _unitOfWork.SaveChangesAsync();
		}

		public override IQueryable<UserConversation> All()
		{
			return _unitOfWork.UserConversations.All().Include(uc => uc.User).Include(uc => uc.Conversation);
		}

		public override async Task<UserConversation> FindByIdAsync(int? id)
		{
			if (id.HasValue)
			{
				return await _unitOfWork.UserConversations.FindByIdAsync(id.Value);
			}
			else
			{
				return null;
			}
		}

		public override async Task DeleteAsync(UserConversation userConversation)
		{
			_unitOfWork.UserConversations.Delete(userConversation);
			await _unitOfWork.SaveChangesAsync();
		}

		public override async Task UpdateAsync(UserConversation userConversation)
		{
			_unitOfWork.UserConversations.Update(userConversation);
			await _unitOfWork.SaveChangesAsync();
		}
	}
}
