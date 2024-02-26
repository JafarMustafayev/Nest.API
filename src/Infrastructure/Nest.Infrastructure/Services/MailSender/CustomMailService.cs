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

    public async Task<List<MailResponseForTableDTO>> GetAllMailsAsync()
    {
        using (var client = new ImapClient())
        {
            await client.ConnectAsync("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

            await client.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);

            client.Inbox.Open(FolderAccess.ReadOnly);
            var uids = client.Inbox.Search(SearchQuery.All);

            var messages = new List<MailResponseForTableDTO>();
            foreach (var uid in uids)
            {
                var message = client.Inbox.GetMessage(uid);

                messages.Add(
                    new MailResponseForTableDTO
                    {
                        MessageId = message.MessageId.Encode(),
                        Body = message.TextBody,
                        Subject = message.Subject,
                        To = message.To.ToString(),
                        From = message.From.ToString(),
                        Date = message.Date.DateTime,
                    });
            }

            await client.DisconnectAsync(true);
            return messages;
        }
    }

    public async Task<SingleMailDTO> GetMailByIdAsync(string id)
    {
        using (var client = new ImapClient())
        {
            await client.ConnectAsync("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

            await client.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);

            await client.Inbox.OpenAsync(FolderAccess.ReadOnly);
            id = id.Decode();

            var uid = await client.Inbox.SearchAsync(SearchQuery.HeaderContains("Message-ID", id));
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
            await client.DisconnectAsync(true);
            return mail;
        }
    }
}