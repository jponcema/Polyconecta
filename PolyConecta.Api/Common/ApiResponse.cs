namespace PolyConecta.Api.Common;

/// <summary>
/// Standardized REST API Response envelope wrapper.
/// </summary>
/// <typeparam name="TData">Payload data type</typeparam>
public class ApiResponse<TData>
{
    public bool Success { get; set; }
    public TData? Data { get; set; }
    public List<string> Errors { get; set; } = new();
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string TraceId { get; set; } = Guid.NewGuid().ToString();

    public static ApiResponse<TData> Ok(TData data) => new()
    {
        Success = true,
        Data = data
    };

    public static ApiResponse<TData> Fail(string error) => new()
    {
        Success = false,
        Errors = new List<string> { error }
    };

    public static ApiResponse<TData> Fail(IEnumerable<string> errors) => new()
    {
        Success = false,
        Errors = errors.ToList()
    };
}
