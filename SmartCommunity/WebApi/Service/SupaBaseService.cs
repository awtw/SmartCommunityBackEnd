using Supabase.Gotrue;
using WebApi.Service.Interface;
using Client = Supabase.Client;

namespace WebApi.Service;

public class SupaBaseService: ISupaBaseService
{
    /// <summary>
    /// _logger
    /// </summary>
    private readonly ILogger _logger;
    
    /// <summary>
    /// _supabase
    /// </summary>
    private readonly Client _supabase;

    public SupaBaseService(ILogger<SupaBaseService> logger, Supabase.Client supabase)
    {
        _logger = logger;
        _supabase = supabase;
    }

    /// <summary>
    /// CreateUser
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns>Session</returns>
    public async Task<Session> CreateUser(string email, string password)
    {
        _logger.LogInformation("Craete User");
        try
        {
            var session = await _supabase.Auth.SignUp(email, password);
            _logger.LogInformation(@"Session: ${session}");
            
            return session;
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            throw;
        }
    }
    
    /// <summary>
    /// Login
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns>Session</returns>
    public async Task<Session> Login(string email, string password)
    {
        _logger.LogInformation("Craete User");
        try
        {
            var session = await _supabase.Auth.SignIn(email, password);
            _logger.LogInformation(@"Session: ${session}");
            
            return session;
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            throw;
        }
    }

    /// <summary>
    /// Logout
    /// </summary>
    /// <returns>bool</returns>
    public async Task<bool> Logout()
    {
        try
        {
            await _supabase.Auth.SignOut();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return false;
        }
        
    }
}