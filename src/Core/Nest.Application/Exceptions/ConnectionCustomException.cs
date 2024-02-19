
namespace Nest.Application.Exceptions;

public class ConnectionCustomException :Exception, IBaseException
{
    public int StatusCode { get; }

    public string CustomMessage { get; }

    public ConnectionCustomException(int statusCode, string message)
    {
        StatusCode = statusCode;
        CustomMessage = message;
    }

    public ConnectionCustomException(string message)
    {
        StatusCode = 500;
        CustomMessage = message;
    }

    public ConnectionCustomException()
    {
        StatusCode = 500;
        CustomMessage = "Connection string not found";
    }
}

