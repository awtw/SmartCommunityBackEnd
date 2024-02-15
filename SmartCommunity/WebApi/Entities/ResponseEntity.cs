namespace WebApi.Enums;

/// <summary>
/// Response Entity
/// </summary>
public class ResponseEntity
{
    /// <summary>
    /// Response Status
    /// </summary>
    public StatusEnum Status { get; set; }
    
    /// <summary>
    /// Response Data
    /// </summary>
    public  object? Data { get; set; }

    /// <summary>
    /// Response Error Message
    /// </summary>
    public string? ErrorMessage { get; set; }
    
}