using System.Net;

namespace _123vendas.Domain.Exceptions;

public static class ExceptionStatusCodes
{
    private static Dictionary<Type, HttpStatusCode> exceptionsStatusCodes = new Dictionary<Type, HttpStatusCode>
        {
            {typeof(ServiceException), HttpStatusCode.InternalServerError},
            {typeof(NotFoundException), HttpStatusCode.NotFound},
        };

    public static HttpStatusCode GetExceptionStatusCode(Exception exception)
    {
        bool exceptionFound = exceptionsStatusCodes.TryGetValue(exception.GetType(), out HttpStatusCode statusCode);
        return exceptionFound ? statusCode : HttpStatusCode.InternalServerError;
    }
}