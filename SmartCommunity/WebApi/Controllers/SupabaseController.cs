using Microsoft.AspNetCore.Mvc;
using Supabase.Gotrue;
using WebApi.Entities;
using WebApi.Enums;
using WebApi.Service.Interface;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class SupabaseController : ControllerBase
{
    /// <summary>
    /// _supaBaseService
    /// </summary>
    private readonly ISupaBaseService _supaBaseService;
    /// <summary>
    /// _logger
    /// </summary>
    private readonly ILogger<SupabaseController> _logger;

    public SupabaseController(ISupaBaseService supaBaseService, ILogger<SupabaseController> logger)
    {
        _supaBaseService = supaBaseService;
        _logger = logger;
    }
    
    /// <summary>
    /// CreateUser
    /// </summary>
    /// <param name="accountInfo"></param>
    /// <returns>ResponseEntity</returns>
    [HttpPost]
    public async Task<ResponseEntity> CreateUser(AccountInfoEntity accountInfo)
    {
        try
        {
            var user = await _supaBaseService.CreateUser(accountInfo.Email, accountInfo.Password);
            _logger.LogInformation(user.ToString());
            return new ResponseEntity
            {
                Status = StatusEnum.Success,
                Data = user,
                ErrorMessage = null
            };;
        }
        catch (Exception e)
        {
            _logger.LogInformation(e.ToString());
            return new ResponseEntity
            {
                Status = StatusEnum.Error,
                Data = null,
                ErrorMessage = e.Message.ToString()
            };
        }
    }
    
    /// <summary>
    /// Login
    /// </summary>
    /// <param name="accountInfo"></param>
    /// <returns>ResponseEntity</returns>
    [HttpPost]
    public async Task<ResponseEntity> Login(AccountInfoEntity accountInfo)
    {
        try
        {
            var user = await _supaBaseService.Login(accountInfo.Email, accountInfo.Password);
            _logger.LogInformation(user.ToString());
            return new ResponseEntity
            {
                Status = StatusEnum.Success,
                Data = user,
                ErrorMessage = null
            };;
        }
        catch (Exception e)
        {
            _logger.LogInformation(e.ToString());
            return new ResponseEntity
            {
                Status = StatusEnum.Error,
                Data = null,
                ErrorMessage = e.Message.ToString()
            };
        }
    }
    
    /// <summary>
    /// Logout
    /// </summary>
    /// <returns>ResponseEntity</returns>
    [HttpPost]
    public async Task<ResponseEntity> Logout()
    {
        try
        {
            var user = await _supaBaseService.Logout();
            _logger.LogInformation(user.ToString());
            return new ResponseEntity
            {
                Status = StatusEnum.Success,
                Data = user,
                ErrorMessage = null
            };;
        }
        catch (Exception e)
        {
            _logger.LogInformation(e.ToString());
            return new ResponseEntity
            {
                Status = StatusEnum.Error,
                Data = null,
                ErrorMessage = e.Message.ToString()
            };
        }
    }
}