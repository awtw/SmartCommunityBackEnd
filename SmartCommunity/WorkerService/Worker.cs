using System.Diagnostics.Tracing;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WorkerService.Entites;

namespace WorkerService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;
    private readonly IHostApplicationLifetime _lifetime;

    public Worker(ILogger<Worker> logger, IConfiguration configuration, IHostApplicationLifetime lifetime)
    {
        _logger = logger;
        _configuration = configuration;
        _lifetime = lifetime;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(10000, stoppingToken);

                var factory = new ConnectionFactory()
                {
                    HostName = _configuration.GetValue<string>("Settings:RabbitMqConnection:Url"),
                    UserName = _configuration.GetValue<string>("Settings:RabbitMqConnection:UserName"),
                    Password = _configuration.GetValue<string>("Settings:RabbitMqConnection:Password"),
                    VirtualHost = _configuration.GetValue<string>("Settings:RabbitMqConnection:VirtualHost"),
                };
                
                _logger.LogInformation(factory.ToString());

                var conn = factory.CreateConnection();
                _logger.LogInformation(conn.ToString());
                // set the heartbeat timeout to 60 seconds
                factory.RequestedHeartbeat = TimeSpan.FromSeconds(300);

                var channel = conn.CreateChannel();

                var connection = channel;
            
                // using (var connection = channel)
                // {
                    channel.QueueDeclare(queue: "SendMessage",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                    _logger.LogInformation("[*] Waiting for messages.");
                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume( "SendMessage",false, consumer);
                    consumer.Received += async (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        _logger.LogInformation($" [x] Received {message} from {connection.ToString()}");
                        _logger.LogWarning($"Message: {message}");
                        //手動 ack
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                        
                        _logger.LogInformation("OK");
                    };
                // }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                // Environment.ExitCode = 1;
                // _lifetime.StopApplication();
            }
            
        }
    }
}