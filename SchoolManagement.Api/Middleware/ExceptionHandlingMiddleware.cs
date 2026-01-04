using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolManagement.Api.DTOs;

namespace SchoolManagement.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DbUpdateException dbEx)
        {
            _logger.LogError(dbEx, "Database update exception: {Message}", dbEx.Message);
            await WriteErrorAsync(context, dbEx.InnerException?.Message ?? "Database update error", StatusCodes.Status409Conflict);
        }
        catch (ValidationException valEx)
        {
            _logger.LogWarning(valEx, "Validation failed: {Message}", valEx.Message);
            await WriteErrorAsync(context, valEx.Message, StatusCodes.Status400BadRequest);
        }
        catch (ArgumentException argEx)
        {
            _logger.LogWarning(argEx, "Bad request: {Message}", argEx.Message);
            await WriteErrorAsync(context, argEx.Message, StatusCodes.Status400BadRequest);
        }
        catch (KeyNotFoundException knfEx)
        {
            _logger.LogWarning(knfEx, "Not found: {Message}", knfEx.Message);
            await WriteErrorAsync(context, knfEx.Message, StatusCodes.Status404NotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
            await WriteErrorAsync(context, "An unexpected error occurred", StatusCodes.Status500InternalServerError);
        }
    }

    private async Task WriteErrorAsync(HttpContext context, string message, int status)
    {
        // Ensure we haven't already started the response
        if (context.Response.HasStarted)
        {
            _logger.LogWarning("The response has already started, the error response will not be written.");
            return;
        }

        context.Response.Clear();
        context.Response.StatusCode = status;
        context.Response.ContentType = "application/json";

        var meta = new { CorrelationId = context.TraceIdentifier };
        var payload = ApiResponse.Failure(message, status);
        payload.Meta = meta;

        await context.Response.WriteAsJsonAsync(payload);
    }
}
