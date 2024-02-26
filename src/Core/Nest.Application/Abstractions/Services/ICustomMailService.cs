namespace Nest.Application.Abstractions.Services;

public interface ICustomMailService
{
    Task SendEmailAsync(MailRequest mailRequest);

    Task SendWelcomeEmailAsync(string to);

    //Task SendEmailForForgotPasswordAsync( string to, string token );

    Task SendEmailForContactAtMomentAsync(string to, string subject);

    Task SendEmailForContactAsync(string to, string subject, string message);

    Task<List<MailResponseForTableDTO>> GetAllMailsAsync();

    Task<SingleMailDTO> GetMailByIdAsync(string id);
}