using System.Threading.Tasks;
using WAD_Application.Models;
using WAD_Application.Repositories.Interfaces;

namespace WAD_Application.Services.Interfaces
{
	public interface IUnitOfWork
	{
		IRepository<UserConversation> UserConversations { get; }
		IRepository<User> Users { get; }
		IRepository<Conversation> Conversations { get; }
		IRepository<Content> Contents { get; }
		IRepository<Message> Messages { get; }

		Task SaveChangesAsync();
	}
}
