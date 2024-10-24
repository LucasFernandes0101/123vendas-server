using _123vendas.Domain.Base;

namespace _123vendas.Domain.Exceptions;

public class SaleAlreadyCanceledException : BaseException
{
    public SaleAlreadyCanceledException(string message) : base(message)
    {
    }
}
