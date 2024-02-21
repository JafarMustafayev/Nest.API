namespace Nest.Application.Abstractions.Services;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);

    Task SendWelcomeEmailAsync(string to);

    //Task SendEmailForForgotPasswordAsync( string to, string token );

    Task SendEmailForContactAtMomentAsync(string to, string subject);

    Task SendEmailForContactAsync(string to, string subject, string message);
}