namespace WebApi.Entities;

public class MessageEntity
{
    /// <summary>
    /// 主題
    /// </summary>
    public string Topic { get; set; }

    /// <summary>
    /// 發送者
    /// </summary>
    public string Sender { get; set; }
    
    /// <summary>
    /// 接收者
    /// </summary>
    public string Receiver { get; set; }

    /// <summary>
    /// 訊息
    /// </summary>
    public string Message { get; set; }
}