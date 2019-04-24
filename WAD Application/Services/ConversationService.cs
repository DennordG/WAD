using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WAD_Application.Models;
using WAD_Application.Services.Interfaces;

namespace WAD_Application.Services
{
	public class ConversationService : AbstractService<Conversation>
	{
		public ConversationService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

		public override async Task AddAsync(Conversation conversation)
		{
			_unitOfWork.Conversations.Add(conversation);
			await _unitOfWork.SaveChangesAsync();
		}

		public override IQueryable<Conversation> All()
		{
			return _unitOfWork.Conversations.All();
		}

		public override async Task DeleteAsync(Conversation conversation)
		{
			_unitOfWork.Conversations.Delete(conversation);
			await _unitOfWork.SaveChangesAsync();
		}

		public override async Task<Conversation> FindByIdAsync(int? id)
		{
			if (id.HasValue)
			{
				return await _unitOfWork.Conversations.FindByIdAsync(id.Value);
			}
			else
			{
				return null;
			}
		}

		public override async Task UpdateAsync(Conversation conversation)
		{
			_unitOfWork.Conversations.Update(conversation);
			await _unitOfWork.SaveChangesAsync();
		}
	}
}
