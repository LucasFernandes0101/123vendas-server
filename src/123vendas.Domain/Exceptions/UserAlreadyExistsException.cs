using _123vendas.Domain.Base;

namespace _123vendas.Domain.Exceptions;

public class UserAlreadyExistsException : BaseException
{
    public UserAlreadyExistsException(string message) : base(message)
    {
    }
}