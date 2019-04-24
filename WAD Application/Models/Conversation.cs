using System.Collections.Generic;

namespace WAD_Application.Models
{
    public class Conversation
    {
        public int ConversationId { get; set; }

        public virtual IList<UserConversation> UserConversations { get; set; }
    }
}
