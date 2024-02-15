using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Service;
using WebApi.Service.Interface;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class MessageController : ControllerBase
{
    /// <summary>
    /// _logger
    /// </summary>
    private readonly ILogger<MessageController> _logger;
    /// <summary>
    /// _messageProducerService
    /// </summary>
    private readonly IMessageProducerService _messageProducerService;


    public MessageController(ILogger<MessageController> logger, IMessageProducerService messageProducerService)
    {
        _logger = logger;
        _messageProducerService = messageProducerService;
    }
    
    /// <summary>
    /// SendMessage
    /// </summary>
    /// <param name="message"></param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    public IActionResult SendMessage(MessageEntity message)
    {
        try
        {
            _messageProducerService.SendMessage(message);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest();
        }
        
    }
}