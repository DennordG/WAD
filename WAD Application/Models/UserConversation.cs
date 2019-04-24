using System.Collections.Generic;

namespace WAD_Application.Models
{
    public class UserConversation
    {
		public int UserConversationId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ConversationId { get; set; }
        public Conversation Conversation { get; set; }

		public virtual IList<Message> Messages { get; set; }
	}
}
