using _123vendas.Domain.Base;

namespace _123vendas.Domain.Exceptions;

public class ItemOutOfStockException : BaseException
{
    public ItemOutOfStockException(string message) : base(message)
    {
    }
}