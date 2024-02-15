namespace WebApi.Service;

public interface IMessageProducerService
{
    /// <summary>
    /// SendMessage
    /// </summary>
    /// <param name="message"></param>
    /// <typeparam name="T"></typeparam>
    void SendMessage<T>(T message);
}