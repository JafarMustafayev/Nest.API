namespace Nest.Infrastructure.Services.MailSender;

public class MailService : IMailService
{
    private readonly MailSettings _mailSettings;

    public MailService(IOptions<MailSettings> mailSettings)
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
}