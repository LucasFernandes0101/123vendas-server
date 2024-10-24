using _123vendas.Domain.Base;

namespace _123vendas.Domain.Exceptions;

public class RabbitMQConnectionException : BaseException
{
    public RabbitMQConnectionException(string message) : base(message)
    {
    }
}