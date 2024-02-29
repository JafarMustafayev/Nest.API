namespace Nest.Application.DTOs.AuthDTOs;

public class ConfirmEmailDTO
{
    public string Email { get; set; }

    public string Token { get; set; }
}