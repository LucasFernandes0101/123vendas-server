using _123vendas.Domain.Base;

namespace _123vendas.Domain.Exceptions;

public class UnauthorizedUserException : BaseException
{
    public UnauthorizedUserException(string message) : base(message)
    {
    }
}