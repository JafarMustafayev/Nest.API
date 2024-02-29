namespace Nest.Application.DTOs.AuthDTOs;

public class VerifyResetTokenDTO
{
    public string? UserId { get; set; }
    public string? ResetToken { get; set; }
}