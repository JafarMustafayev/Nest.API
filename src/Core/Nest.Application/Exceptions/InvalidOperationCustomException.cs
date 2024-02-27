namespace Nest.Application.Exceptions;

internal class InvalidOperationCustomException : Exception, IBaseException
{
    public int StatusCode { get; }

    public string CustomMessage { get; }

    public InvalidOperationCustomException()
    {
        StatusCode = 400;
        CustomMessage = "Invalid operation";
    }

    public InvalidOperationCustomException(string message, int statusCode = 400)
    {
        StatusCode = statusCode;
        CustomMessage = message;
    }
}