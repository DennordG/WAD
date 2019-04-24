namespace WAD_Application.Models
{
    public class Content
    {
        public int ContentId { get; set; }
        public string TextContent { get; set; }
        public byte[] ImageContent { get; set; }

		public int MessageId { get; set; }
		public Message Message { get; set; }
    }
}