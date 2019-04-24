using System.Threading.Tasks;
using WAD_Application.Data;
using WAD_Application.Models;
using WAD_Application.Repositories.Interfaces;
using WAD_Application.Services.Interfaces;

namespace WAD_Application.Services
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;

		public IRepository<UserConversation> UserConversations { get; }
		public IRepository<User> Users { get; }
		public IRepository<Conversation> Conversations { get; }
		public IRepository<Content> Contents { get; }
		public IRepository<Message> Messages { get; }

		public UnitOfWork(ApplicationDbContext context,
			IRepository<UserConversation> userConversations,
			IRepository<User> users,
			IRepository<Conversation> conversations,
			IRepository<Content> contents,
			IRepository<Message> messages)
		{
			_context = context;
			UserConversations = userConversations;
			Users = users;
			Conversations = conversations;
			Contents = contents;
			Messages = messages;
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
