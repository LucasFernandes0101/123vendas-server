using _123vendas.Domain.Base;

namespace _123vendas.Domain.Exceptions;

public class EntityAlreadyDeletedException : BaseException
{
    public EntityAlreadyDeletedException(string message) : base(message)
    {
    }
}