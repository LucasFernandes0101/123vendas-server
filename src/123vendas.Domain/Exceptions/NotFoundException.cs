using _123vendas.Domain.Base;

namespace _123vendas.Domain.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string message) : base (message)
    {
    }
}