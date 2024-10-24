using _123vendas.Domain.Base;
using _123vendas.Domain.Exceptions;
using _123vendas.Domain.Interfaces.Integrations;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;

namespace _123vendas.Infrastructure.Integrations;

public class RabbitMQIntegration : IRabbitMQIntegration, IDisposable
{
    private readonly IConnectionFactory _connectionFactory;
    private IConnection? _persistentConnection;
    private IModel? _channel;

    public RabbitMQIntegration()
    {
        _connectionFactory = new ConnectionFactory
        {
            HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOSTNAME"),
            UserName = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME"),
            VirtualHost = Environment.GetEnvironmentVariable("RABBITMQ_VIRTUALHOST"),
            Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD")
        };
    }

    public async Task PublishMessageAsync(BaseEvent @event)
    {
        EnsureConnected();

        string exchangeName = $"ex_{@event.Domain.ToLower()}";
        _channel.ExchangeDeclare(exchangeName, "direct", durable: true);

        string routingKey = @event.GetType().Name;
        string message = JsonConvert.SerializeObject(@event);
        byte[] body = Encoding.UTF8.GetBytes(message);

        var basicProperties = _channel.CreateBasicProperties();
        basicProperties.Persistent = true;

        for (int retry = 0; retry < 10; retry++)
        {
            try
            {
                _channel.BasicPublish(
                    exchange: exchangeName,
                    routingKey: routingKey,
                    mandatory: true,
                    basicProperties: basicProperties,
                    body: body
                );

                return;
            }
            catch (AlreadyClosedException ex)
            {
                await HandlePublishErrorAsync(ex, retry, "Connection already closed.");
            }
            catch (BrokerUnreachableException ex)
            {
                await HandlePublishErrorAsync(ex, retry, "Broker unreachable.");
            }
            catch (Exception ex)
            {
                await HandlePublishErrorAsync(ex, retry, "Unknown error occurred while publishing.");
            }
        }

        throw new RabbitMQMessageException("Failed to publish message after multiple attempts.");
    }

    private void EnsureConnected()
    {
        if (_persistentConnection == null || !_persistentConnection.IsOpen)
            _persistentConnection = TryConnect(_connectionFactory);

        if (_channel is null || !_channel.IsOpen)
            _channel = _persistentConnection.CreateModel();
    }

    private async Task HandlePublishErrorAsync(Exception ex, int retry, string message)
    {
        if (retry == 9)
        {
            throw new RabbitMQMessageException($"{message} Error: {ex.Message}", ex);
        }

        await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, retry)));
    }

    private IConnection TryConnect(IConnectionFactory connectionFactory)
    {
        string errorMessage = string.Empty;

        for (int attempt = 0; attempt < 4; attempt++)
        {
            try
            {
                return connectionFactory.CreateConnection();
            }
            catch (BrokerUnreachableException ex)
            {
                errorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, attempt))).Wait();
        }

        throw new RabbitMQConnectionException($"Failed to connect to RabbitMQ after multiple attempts. Error: {errorMessage}");
    }

    public void Dispose()
    {
        _channel?.Close();
        _persistentConnection?.Close();
        _channel?.Dispose();
        _persistentConnection?.Dispose();
    }
}
