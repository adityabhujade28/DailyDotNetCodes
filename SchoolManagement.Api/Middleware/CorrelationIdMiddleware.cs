using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace SchoolManagement.Api.Middleware;

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;
    private const string HeaderName = "X-Correlation-ID";

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Request.Headers.ContainsKey(HeaderName)
            ? context.Request.Headers[HeaderName].ToString()
            : Activity.Current?.Id ?? Guid.NewGuid().ToString();

        // Set in response headers (use indexer to avoid duplicate-key exceptions)
        context.Response.OnStarting(() => {
            context.Response.Headers[HeaderName] = correlationId; 
            return Task.CompletedTask;
        });

        // Add to trace identifier
        context.TraceIdentifier = correlationId;

        // Create logging scope
        using (context.RequestServices.GetService(typeof(Microsoft.Extensions.Logging.ILogger<CorrelationIdMiddleware>)) is Microsoft.Extensions.Logging.ILogger logger ? logger.BeginScope(new Dictionary<string, object>{{"CorrelationId", correlationId}}) : null)
        {
            await _next(context);
        }
    }
}