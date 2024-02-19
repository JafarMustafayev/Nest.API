namespace Nest.Application.Abstractions.Services;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}