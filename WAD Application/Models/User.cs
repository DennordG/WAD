using System.Collections.Generic;

namespace WAD_Application.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public bool IsOnline { get; set; }

        public virtual IList<UserConversation> UserConversations { get; set; }
    }
}
