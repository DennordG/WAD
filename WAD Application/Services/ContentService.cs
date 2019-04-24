using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WAD_Application.Models;
using WAD_Application.Services.Interfaces;

namespace WAD_Application.Services
{
	public class ContentService : AbstractService<Content>
	{
		public ContentService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

		public override async Task AddAsync(Content content)
		{
			_unitOfWork.Contents.Add(content);
			await _unitOfWork.SaveChangesAsync();
		}

		public override IQueryable<Content> All()
		{
			return _unitOfWork.Contents.All().Include(c => c.Message);
		}

		public override async Task<Content> FindByIdAsync(int? id)
		{
			if (id.HasValue)
			{
				return await _unitOfWork.Contents.FindByIdAsync(id.Value);
			}
			else
			{
				return null;
			}
		}

		public override async Task DeleteAsync(Content content)
		{
			_unitOfWork.Contents.Delete(content);
			await _unitOfWork.SaveChangesAsync();
		}

		public override async Task UpdateAsync(Content content)
		{
			_unitOfWork.Contents.Update(content);
			await _unitOfWork.SaveChangesAsync();
		}
	}
}
