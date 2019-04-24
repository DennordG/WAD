using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WAD_Application.Models;
using WAD_Application.Services.Interfaces;

namespace WAD_Application.Services
{
	public class MessageService : AbstractService<Message>
	{
		public MessageService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

		public override async Task AddAsync(Message message)
		{
			_unitOfWork.Messages.Add(message);
			await _unitOfWork.SaveChangesAsync();
		}

		public override IQueryable<Message> All()
		{
			return _unitOfWork.Messages.All().Include(m => m.UserConversation);
		}

		public override async Task DeleteAsync(Message message)
		{
			_unitOfWork.Messages.Delete(message);
			await _unitOfWork.SaveChangesAsync();
		}

		public override async Task<Message> FindByIdAsync(int? id)
		{
			if (id.HasValue)
			{
				return await _unitOfWork.Messages.FindByIdAsync(id.Value);
			}
			else
			{
				return null;
			}
		}

		public override async Task UpdateAsync(Message message)
		{
			_unitOfWork.Messages.Update(message);
			await _unitOfWork.SaveChangesAsync();
		}
	}
}
