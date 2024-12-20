using BitmailServer.Models;
using MimeKit;

namespace BitmailServer.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(EmailDto email);
        List<InboxItemDto> GetInbox(int page, int count);

        Task SendEmailAsync(EmailDto email);
        Task<List<InboxItemDto>> GetInboxAsync(int page, int count);
    }
}
