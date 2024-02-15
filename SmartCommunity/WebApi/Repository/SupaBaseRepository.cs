using WebApi.Repository.Interface;

namespace WebApi.Repository;

public class SupaBaseRepository : ISupaBaseRepository
{
    /// <summary>
    /// Logger
    /// </summary>
    private readonly ILogger _logger;
    public SupaBaseRepository(ILogger<SupaBaseRepository> logger)
    {
        _logger = logger;
    }
}