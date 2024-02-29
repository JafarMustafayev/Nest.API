namespace Nest.Application.Abstractions.Services;

public interface ICustomMailService
{
    Task SendEmailAsync(MailRequest mailRequest);

    Task SendWelcomeEmailAsync(string to, string? confirmationUrl = null);

    Task SendEmailForForgotPasswordAsync(string to, string url);

    Task SendEmailForContactAtMomentAsync(string to, string subject);

    Task SendEmailForContactAsync(string to, string subject, string message);

    Task<ResponseDTO> GetAllMailsAsync(int page = 1, int take = 20);

    Task<ResponseDTO> GetMailByIdAsync(string id);

    Task<ResponseDTO> DeleteMessage(string id);

    Task<ResponseDTO> RecycleBins();
}