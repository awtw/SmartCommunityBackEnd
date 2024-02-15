using Supabase.Gotrue;

namespace WebApi.Service.Interface;

public interface ISupaBaseService
{
 
    /// <summary>
    /// CreateUser
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns>Session</returns>
    public Task<Session> CreateUser(string email, string password);

    /// <summary>
    /// Login
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns>Session</returns>
    public Task<Session> Login(string email, string password);

    /// <summary>
    /// Logout
    /// </summary>
    /// <returns>bool</returns>
    public Task<bool> Logout();
}