using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using SchoolManagement.Api.DTOs;
using SchoolManagement.Api.Exceptions;

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
        catch (ConflictException confEx)
        {
            _logger.LogWarning(confEx, "Conflict: {Message}", confEx.Message);
            await WriteErrorAsync(context, confEx.Message, StatusCodes.Status409Conflict, new { Detail = confEx.Message });
        }
        catch (DbUpdateException dbEx)
        {
            _logger.LogError(dbEx, "Database update exception: {Message}", dbEx.Message);

            // Detect SQL Server unique constraint violations (2601, 2627)
            var inner = dbEx.InnerException;
            if (inner is SqlException sqlEx)
            {
                // FK violation (delete restricted by dependent rows)
                if (sqlEx.Number == 547)
                {
                    _logger.LogWarning(dbEx, "Foreign key violation (delete restricted): {Message}", sqlEx.Message);
                    await WriteErrorAsync(context, "Delete failed: dependent records exist.", StatusCodes.Status409Conflict, new { Detail = "Dependent child records prevent delete (foreign key constraint)" });
                    return;
                }

                // Detect SQL Server unique constraint violations (2601, 2627)
                if (sqlEx.Number == 2601 || sqlEx.Number == 2627)
                {
                    // Try to extract index name and duplicate value from the server message using a forgiving approach
                    var messageText = inner.Message ?? string.Empty;

                    // First try the detailed pattern (index name + duplicate value) used on many SQL Server messages
                    var match = Regex.Match(messageText, @"unique (?:index|constraint) '([^']+)'[\s\S]*duplicate key value is \(([^)]+)\)", RegexOptions.IgnoreCase);

                    string? indexName = null;
                    string? duplicateValue = null;

                    if (match.Success)
                    {
                        indexName = match.Groups[1].Value;
                        duplicateValue = match.Groups[2].Value;
                    }
                    else
                    {
                        // Fallback: try to find a likely index name (e.g., IX_Students_Email) anywhere in the message
                        var idxMatch = Regex.Match(messageText, @"(IX_[A-Za-z0-9_]+|ix_[A-Za-z0-9_]+|students_email|students_email_idx)", RegexOptions.IgnoreCase);
                        if (idxMatch.Success)
                        {
                            indexName = idxMatch.Value;
                        }

                        // Fallback: capture the first parenthesized value as the duplicate value
                        var dupMatch = Regex.Match(messageText, @"\(([^)]+)\)");
                        if (dupMatch.Success)
                        {
                            duplicateValue = dupMatch.Groups[1].Value;
                        }
                    }

                    var friendlyMessage = "A resource with the same unique key already exists.";
                    object? errors = null;

                    if (!string.IsNullOrEmpty(indexName))
                    {
                        // Map known index names to field names for friendly errors
                        if (indexName.IndexOf("departments_name", StringComparison.OrdinalIgnoreCase) >= 0 || indexName.IndexOf("ix_departments_name", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            friendlyMessage = "Department with the same name already exists.";
                            errors = new { Name = $"'{duplicateValue ?? "value"}' is already in use" };
                        }
                        else if (indexName.IndexOf("courses_title", StringComparison.OrdinalIgnoreCase) >= 0 || indexName.IndexOf("ix_courses_title", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            friendlyMessage = "Course with the same title already exists.";
                            errors = new { Title = $"'{duplicateValue ?? "value"}' is already in use" };
                        }
                        else if (indexName.IndexOf("students_email", StringComparison.OrdinalIgnoreCase) >= 0 || indexName.IndexOf("ix_students_email", StringComparison.OrdinalIgnoreCase) >= 0 || indexName.IndexOf("ix_students_email", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            // Friendly message for duplicate student emails; include the offending email in the errors object
                            friendlyMessage = "A student with the same email already exists.";
                            errors = new { Email = $"'{duplicateValue ?? "the value"}' is already registered" };
                        }
                        else
                        {
                            // Unknown index but we have an index name; include duplicate value when possible
                            errors = new { duplicateValue = duplicateValue };
                        }
                    }
                    else if (!string.IsNullOrEmpty(duplicateValue))
                    {
                        // No index name found but we got a duplicate value; provide it back to the caller
                        errors = new { duplicateValue = duplicateValue };
                    }
                    else
                    {
                        // Generic feedback when parsing fails
                        friendlyMessage = "Duplicate key violates unique constraint.";
                    }

                    await WriteErrorAsync(context, friendlyMessage, StatusCodes.Status409Conflict, errors);
                    return;
                }
            }

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

    private async Task WriteErrorAsync(HttpContext context, string message, int status, object? errors = null)
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

        // Always include the Errors property (null when none) so the anonymous type shape is consistent
        var meta = new { CorrelationId = context.TraceIdentifier, Errors = errors };
        var payload = ApiResponse.Failure(message, status);
        payload.Meta = meta;

        await context.Response.WriteAsJsonAsync(payload);
    }
}
