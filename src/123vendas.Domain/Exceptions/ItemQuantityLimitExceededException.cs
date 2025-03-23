using _123vendas.Domain.Base;

namespace _123vendas.Domain.Exceptions;

public class ItemQuantityLimitExceededException : BaseException
{
    public ItemQuantityLimitExceededException(string message) : base(message)
    {
    }
}