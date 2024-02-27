using MimeKit.Cryptography;
using System.Linq.Expressions;

namespace Nest.Infrastructure.Services.MailSender;

public class CustomMailService : ICustomMailService
{
    private readonly MailSettings _mailSettings;

    public CustomMailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        var email = new MimeMessage();

        email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
        email.To.Add(MailboxAddress.Parse(mailRequest.To));
        email.Subject = mailRequest.Subject;
        var builder = new BodyBuilder();
        if (mailRequest.Attachments != null)
        {
            byte[] fileBytes;
            foreach (var file in mailRequest.Attachments)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                }
            }
        }
        builder.HtmlBody = mailRequest.Body;
        email.Body = builder.ToMessageBody();
        using var smtp = new SmtpClient();
        smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);
    }

    public async Task SendEmailForContactAsync(string to, string subject, string message)
    {
        MailRequest request = new()
        {
            To = to,
            Subject = subject,
            Body = message
        };

        await SendEmailAsync(request);
    }

    public async Task SendEmailForContactAtMomentAsync(string to, string subject)
    {
        MailRequest request = new()
        {
            To = to,
            Subject = subject,
            Body = "<h1>Thank you for contacting us</h1>"
        };

        await SendEmailAsync(request);
    }

    public async Task SendWelcomeEmailAsync(string to)
    {
        MailRequest request = new()
        {
            To = to,
            Subject = "Welcome to Nest",
            Body = "<h1>Welcome to Nest</h1>"
        };

        await SendEmailAsync(request);
    }

    public async Task<ResponseDTO> GetAllMailsAsync(int page = 1, int take = 20)
    {
        using (var client = new ImapClient())
        {
            await client.ConnectAsync("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

            await client.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);

            await client.Inbox.OpenAsync(FolderAccess.ReadOnly);
            var seenUids = await client.Inbox.SearchAsync(SearchQuery.Seen);
            var unSeenUids = await client.Inbox.SearchAsync(SearchQuery.NotSeen);

            var messages = new List<MailResponseForTableDTO>();

            messages.AddRange(await AddMessageToList(seenUids, true, client));
            messages.AddRange(await AddMessageToList(unSeenUids, false, client));

            await client.DisconnectAsync(true);

            messages = messages.OrderByDescending(x => x.Date).Skip((page - 1) * take).Take(take).ToList();

            return new()
            {
                Message = "Email sent successfully",
                Success = true,
                StatusCode = 200,
                Payload = messages
            };
        }
    }

    public async Task<ResponseDTO> GetMailByIdAsync(string id)
    {
        using (var client = new ImapClient())
        {
            await client.ConnectAsync("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

            await client.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);

            await client.Inbox.OpenAsync(FolderAccess.ReadWrite);

            var uid = await client.Inbox.SearchAsync(SearchQuery.HeaderContains("Message-ID", id.Decode()));

            if (uid.Count == 0)
            {
                throw new NotFoundCustomException("Mail not found");
            }

            var message = await client.Inbox.GetMessageAsync(uid[0]);
            var mail = new SingleMailDTO
            {
                To = message.To.ToString(),
                From = message.From.ToString(),
                Subject = message.Subject,
                Body = message.TextBody,
                Date = message.Date.DateTime,
                MessageId = message.MessageId.Encode(),
                Bcc = message.Bcc.ToString(),
                Cc = message.Cc.ToString(),
                ReplyTo = message.ReplyTo.ToString(),
                Importance = message.Importance.ToString(),
            };

            client.Inbox.AddFlags(uid, MessageFlags.Seen, true);
            await client.DisconnectAsync(true);
            return new()
            {
                Errors = null,
                Message = "Mail found",
                Payload = mail,
                Success = true,
                StatusCode = 200
            };
        }
    }

    public async Task<ResponseDTO> DeleteMessage(string id)
    {
        using (var client = new ImapClient())
        {
            await client.ConnectAsync("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

            await client.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);

            await client.Inbox.OpenAsync(FolderAccess.ReadWrite);

            var uid = await client.Inbox.SearchAsync(SearchQuery.HeaderContains("Message-ID", id.Decode()));

            if (uid.Count == 0)
            {
                throw new NotFoundCustomException("Mail not found");
            }

            await client.Inbox.AddFlagsAsync(uid, MessageFlags.Deleted, true);

            await client.Inbox.ExpungeAsync();

            await client.DisconnectAsync(true);
        }
        return new ResponseDTO
        {
            Message = "Mail deleted successfully",
            Success = true,
            StatusCode = 200
        };
    }

    public async Task<ResponseDTO> RecycleBins()
    {
        using (var client = new ImapClient())
        {
            await client.ConnectAsync("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

            await client.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);

            await client.Inbox.OpenAsync(FolderAccess.ReadOnly);
            var seenUids = await client.Inbox.SearchAsync(SearchQuery.Deleted);
            //var unSeenUids = await client.Inbox.SearchAsync(SearchQuery.Deleted.And(SearchQuery.NotSeen));

            var messages = new List<MailResponseForTableDTO>();

            messages.AddRange(await AddMessageToList(seenUids, true, client));
            //messages.AddRange(await AddMessageToList(unSeenUids, false, client));

            await client.DisconnectAsync(true);

            return new()
            {
                Message = "Recycle Bins",
                Success = true,
                StatusCode = 200,
                Payload = messages
            };
        }
    }

    private async Task<List<MailResponseForTableDTO>> AddMessageToList(IList<UniqueId> uids, bool IsSeen, ImapClient client)
    {
        var messages = new List<MailResponseForTableDTO>();
        foreach (var uid in uids)
        {
            var message = await client.Inbox.GetMessageAsync(uid);

            messages.Add(
                new MailResponseForTableDTO
                {
                    MessageId = message.MessageId.Encode(),
                    Body = message.TextBody,
                    Subject = message.Subject,
                    To = message.To.ToString(),
                    From = message.From.ToString(),
                    Date = message.Date.DateTime,
                    IsSeen = IsSeen
                });
        }

        return messages;
    }
}