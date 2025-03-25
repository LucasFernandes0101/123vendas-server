using _123vendas.Domain.Base;

namespace _123vendas.Domain.Exceptions;

public class ServiceException : BaseException
{
    public ServiceException(string message, Exception innerException) : base(message, innerException)
    {
    }
}