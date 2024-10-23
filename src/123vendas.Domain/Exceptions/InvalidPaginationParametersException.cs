using _123vendas.Domain.Base;

namespace _123vendas.Domain.Exceptions;

public class InvalidPaginationParametersException : BaseException
{
    public InvalidPaginationParametersException(string message) : base(message)
    {
    }
}