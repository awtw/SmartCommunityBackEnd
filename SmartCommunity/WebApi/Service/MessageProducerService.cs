using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using WebApi.Entities;

namespace WebApi.Service;

public class MessageProducerService : IMessageProducerService
{
    /// <summary>
    /// _configuration
    /// </summary>
    private readonly IConfiguration _configuration;

    public MessageProducerService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    /// <summary>
    /// SendMessage
    /// </summary>
    /// <param name="message"></param>
    /// <typeparam name="T"></typeparam>
    public void SendMessage<T>(T message)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _configuration.GetValue<string>("Settings:RabbitMqConnection:Url"),
            UserName = _configuration.GetValue<string>("Settings:RabbitMqConnection:UserName"),
            Password = _configuration.GetValue<string>("Settings:RabbitMqConnection:Password"),
            VirtualHost = _configuration.GetValue<string>("Settings:RabbitMqConnection:VirtualHost"),
        };
        var conn = factory.CreateConnection();

        using var channel = conn.CreateChannel();

        // channel.QueueDeclare("SendMessage", durable: true, exclusive: true);

        var jsonString = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(jsonString);
        
        channel.BasicPublish("SmartCommunity", "SmartCommunity-SendMessage", body: body);
    }
}