namespace BitmailServer.Models
{
    public class InboxItemDto
    {
        public EmailAndName From { get; set; }
        public string Subject { get; set; }
        public string Date { get; set; }
        public string BodyPreview { get; set; }
    }
}
