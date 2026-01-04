using System.Text.Json.Serialization;

namespace SchoolManagement.Api.DTOs;

/// <summary>
/// Standard API response wrapper for typed responses
/// </summary>
/// <typeparam name="T">Payload type</typeparam>
public class ApiResponse<T>
{
    /// <summary>Whether the request was successful</summary>
    public bool Success { get; set; }

    /// <summary>Response payload</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }

    /// <summary>Error or informational message</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }

    /// <summary>HTTP status code</summary>
    public int Status { get; set; }

    /// <summary>Response timestamp (UTC)</summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>Additional metadata</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Meta { get; set; }

    public static ApiResponse<T> SuccessResponse(T data, int status = 200, object? meta = null)
        => new ApiResponse<T> { Success = true, Data = data, Status = status, Meta = meta };

    public static ApiResponse<T> Failure(string message, int status = 400)
        => new ApiResponse<T> { Success = false, Message = message, Status = status };
}

/// <summary>
/// Standard API response wrapper for non-typed responses
/// </summary>
public class ApiResponse
{
    /// <summary>Whether the request was successful</summary>
    public bool Success { get; set; }

    /// <summary>Error or informational message</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }

    /// <summary>HTTP status code</summary>
    public int Status { get; set; }

    /// <summary>Response timestamp (UTC)</summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>Additional metadata</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Meta { get; set; }

    public static ApiResponse SuccessResponse(int status = 200, object? meta = null)
        => new ApiResponse { Success = true, Status = status, Meta = meta };

    public static ApiResponse Failure(string message, int status = 400)
        => new ApiResponse { Success = false, Message = message, Status = status };
}