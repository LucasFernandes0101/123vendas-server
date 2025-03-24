using _123vendas.Domain.Base;

namespace _123vendas.Domain.Exceptions;

public class BadRequestException : BaseException
{
    public BadRequestException(string message) : base(message)
    {
    }
}