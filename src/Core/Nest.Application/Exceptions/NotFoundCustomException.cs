namespace Nest.Application.Exceptions;

public class NotFoundCustomException : Exception, IBaseException
{
    public int StatusCode { get; }

    public string CustomMessage { get; }

    public NotFoundCustomException(string message, int statusCode = 404)
    {
        StatusCode = statusCode;
        CustomMessage = message;
    }
}