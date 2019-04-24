namespace WAD_Application.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public int UserConversationId { get; set; }
        public bool IsRead { get; set; }

        public virtual Content Content { get; set; }

		public virtual UserConversation UserConversation { get; set; }
    }
}