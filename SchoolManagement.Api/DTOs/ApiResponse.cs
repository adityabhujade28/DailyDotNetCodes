using System.Text.Json.Serialization;

namespace SchoolManagement.Api.DTOs;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }
    public int Status { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Meta { get; set; }

    public static ApiResponse<T> SuccessResponse(T data, int status = 200, object? meta = null)
        => new ApiResponse<T> { Success = true, Data = data, Status = status, Meta = meta };

    public static ApiResponse<T> Failure(string message, int status = 400)
        => new ApiResponse<T> { Success = false, Message = message, Status = status };
}

public class ApiResponse
{
    public bool Success { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }
    public int Status { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Meta { get; set; }

    public static ApiResponse SuccessResponse(int status = 200, object? meta = null)
        => new ApiResponse { Success = true, Status = status, Meta = meta };

    public static ApiResponse Failure(string message, int status = 400)
        => new ApiResponse { Success = false, Message = message, Status = status };
}