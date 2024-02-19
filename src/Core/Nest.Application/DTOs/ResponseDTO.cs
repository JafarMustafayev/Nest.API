namespace Nest.Application.DTOs;

public class ResponseDTO
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public bool Success { get; set; }
    public string[]? Errors { get; set; }
    public object? Data { get; set; }
}