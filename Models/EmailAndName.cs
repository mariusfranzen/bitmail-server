using MimeKit;

namespace BitmailServer.Models
{
    public class EmailAndName
    {
        public required string Email { get; set; }
        public required string Name { get; set; }

        public MailboxAddress ToMailboxAddress()
        {
            return new MailboxAddress(Name, Email);
        }
    }
}
