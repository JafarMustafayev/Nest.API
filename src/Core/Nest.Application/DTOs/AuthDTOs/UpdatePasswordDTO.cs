namespace Nest.Application.DTOs.AuthDTOs;

public class UpdatePasswordDTO
{
    public string? UserId { get; set; }

    public string? NewPassword { get; set; }

    public string? ResetToken { get; set; }
}