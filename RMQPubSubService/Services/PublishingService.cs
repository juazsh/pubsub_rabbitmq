using System.Text;
using RabbitMQ.Client;

namespace RMQPubSubService.Services;

public class PublishingService
{
    private readonly string _queueName = "rmQueue";

    public async Task Publish(string message)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();
        await channel.QueueDeclareAsync(_queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        var body = Encoding.UTF8.GetBytes(message);
        var props = new BasicProperties
        {
            ContentType = "text/plain"
        };
        await channel.BasicPublishAsync(exchange: "",routingKey:_queueName, mandatory: false, basicProperties: props, body: body);
    }
}