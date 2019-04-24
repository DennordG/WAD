using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WAD_Application.Models;

namespace WAD_Application.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> AppUsers { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<UserConversation> UserConversations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Content> Contents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserConversation>().HasOne<User>(uc => uc.User)
                .WithMany(u => u.UserConversations).HasForeignKey(uc => uc.UserId);
            builder.Entity<UserConversation>().HasOne<Conversation>(uc => uc.Conversation)
                .WithMany(u => u.UserConversations).HasForeignKey(uc => uc.ConversationId);
		}
    }
}
