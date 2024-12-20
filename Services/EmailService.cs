using BitmailServer.Interfaces;
using BitmailServer.Models;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace BitmailServer.Services
{
    public class EmailService : IEmailService
    {
        // TODO: Set mail and password to the user from security stuff
        const string SmtpServer = "mailcluster.loopia.se";
        const string ImapServer = "mailcluster.loopia.se";
        const string Name = "Marius Franzen";
        const string EmailAddress = "mail@mariusfranzen.se";
        const string Password = "VakttornetMariusFranzen1";

        public void SendEmail(EmailDto email)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(Name, EmailAddress));

            if (email.ToEmail.Count == 0)
            {
                throw new ArgumentException("List of receivers must not be empty", nameof(email.ToEmail));
            }

            foreach (var to in email.ToEmail)
            {
                message.To.Add(to.ToMailboxAddress());
            }

            using var client = new SmtpClient();
            client.Connect(SmtpServer, 0, SecureSocketOptions.SslOnConnect);
            client.Authenticate(EmailAddress, Password);
            client.Send(message);
            client.Disconnect(true);
        }

        public List<MimeMessage> GetInbox(int page, int count)
        {
            using var client = new ImapClient();
            client.Connect(ImapServer, 0, SecureSocketOptions.SslOnConnect);
            client.Authenticate(EmailAddress, Password);

            client.Inbox.Open(FolderAccess.ReadOnly);
            List<MimeMessage> messages = client.Inbox.Skip(page * count).Take(count).ToList();

            return messages;
        }

        public async Task SendEmailAsync(EmailDto email)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(Name, EmailAddress));

            if (email.ToEmail.Count == 0)
            {
                throw new ArgumentException("List of receivers must not be empty", nameof(email.ToEmail));
            }

            foreach (var to in email.ToEmail)
            {
                message.To.Add(to.ToMailboxAddress());
            }

            using var client = new SmtpClient();
            await client.ConnectAsync(SmtpServer, 0, SecureSocketOptions.SslOnConnect);
            await client.AuthenticateAsync(EmailAddress, Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        public async Task<List<InboxItemDto>> GetInboxAsync(int page, int count)
        {
            using var client = new ImapClient();
            await client.ConnectAsync(ImapServer, 0, SecureSocketOptions.SslOnConnect);
            await client.AuthenticateAsync(EmailAddress, Password);
            await client.Inbox.OpenAsync(FolderAccess.ReadOnly);

            List<MimeMessage> mimeMessages = client.Inbox.Skip(page * count).Take(count).ToList();
            List<InboxItemDto> messages = [];

            foreach (MimeMessage mimeMessage in mimeMessages)
            {
                var message = new InboxItemDto
                {
                    From = mimeMessage.From.Mailboxes.Select(m => MailboxAddressToEmailAndName(m)).FirstOrDefault(),
                };
            }

            return messages;
        }

        private static EmailAndName MailboxAddressToEmailAndName(MailboxAddress mailboxAddress)
        {
            return new EmailAndName
            {
                Name = mailboxAddress.Name,
                Email = mailboxAddress.Address
            };
        }

        private static EmailAndName InternetAddressToEmailAndName(InternetAddress internetAddress)
        {
            return new EmailAndName
            {
                Name = string.Empty,
                Email = internetAddress.
            };
        }
    }
}
