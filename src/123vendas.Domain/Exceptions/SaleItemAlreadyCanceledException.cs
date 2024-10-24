using _123vendas.Domain.Base;

namespace _123vendas.Domain.Exceptions;

public class SaleItemAlreadyCanceledException : BaseException
{
    public SaleItemAlreadyCanceledException(string message) : base(message)
    {
    }
}
