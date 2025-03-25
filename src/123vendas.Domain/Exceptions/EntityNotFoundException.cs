using _123vendas.Domain.Base;

namespace _123vendas.Domain.Exceptions;

public class EntityNotFoundException : BaseException
{
    public EntityNotFoundException(string message) : base(message)
    {
    }
}