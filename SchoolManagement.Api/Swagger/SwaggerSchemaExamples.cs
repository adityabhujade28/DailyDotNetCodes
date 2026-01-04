using System.Text.Json;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using SchoolManagement.Api.DTOs;

namespace SchoolManagement.Api.Swagger;

public class SwaggerSchemaExamples : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var type = context.Type;

        // ApiResponse<T>
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ApiResponse<>))
        {
            var inner = type.GetGenericArguments()[0];
            var example = new OpenApiObject
            {
                ["success"] = new OpenApiBoolean(true),
                ["status"] = new OpenApiInteger(200),
                ["timestamp"] = new OpenApiString(DateTime.UtcNow.ToString("o"))
            };

            // simple examples for known DTO types
            if (inner.Name == "CourseDto")
            {
                example["data"] = new OpenApiObject
                {
                    ["id"] = new OpenApiInteger(123),
                    ["title"] = new OpenApiString("Intro to Algorithms"),
                    ["description"] = new OpenApiString("Algorithms and data structures"),
                    ["credits"] = new OpenApiInteger(4),
                    ["departmentId"] = new OpenApiInteger(1),
                    ["departmentName"] = new OpenApiString("Computer Science")
                };
            }
            else if (inner.IsGenericType && inner.GetGenericTypeDefinition() == typeof(PagedResult<>))
            {
                var itemType = inner.GetGenericArguments()[0];
                var items = new OpenApiArray();
                if (itemType.Name == "CourseDto")
                {
                    var item = new OpenApiObject
                    {
                        ["id"] = new OpenApiInteger(123),
                        ["title"] = new OpenApiString("Intro to Algorithms"),
                        ["description"] = new OpenApiString("Algorithms and data structures"),
                        ["credits"] = new OpenApiInteger(4),
                        ["departmentId"] = new OpenApiInteger(1),
                        ["departmentName"] = new OpenApiString("Computer Science")
                    };
                    items.Add(item);
                }

                var paged = new OpenApiObject
                {
                    ["items"] = items,
                    ["page"] = new OpenApiInteger(1),
                    ["pageSize"] = new OpenApiInteger(10),
                    ["totalCount"] = new OpenApiInteger(1),
                    ["totalPages"] = new OpenApiInteger(1)
                };

                example["data"] = paged;
            }
            else
            {
                // generic example
                example["data"] = new OpenApiString("...sample data...");
            }

            schema.Example = example;
        }

        // PagedResult<T> example
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(PagedResult<>))
        {
            var itemType = type.GetGenericArguments()[0];
            var items = new OpenApiArray();
            if (itemType.Name == "CourseDto")
            {
                var item = new OpenApiObject
                {
                    ["id"] = new OpenApiInteger(123),
                    ["title"] = new OpenApiString("Intro to Algorithms"),
                    ["description"] = new OpenApiString("Algorithms and data structures"),
                    ["credits"] = new OpenApiInteger(4),
                    ["departmentId"] = new OpenApiInteger(1),
                    ["departmentName"] = new OpenApiString("Computer Science")
                };
                items.Add(item);
            }

            var paged = new OpenApiObject
            {
                ["items"] = items,
                ["page"] = new OpenApiInteger(1),
                ["pageSize"] = new OpenApiInteger(10),
                ["totalCount"] = new OpenApiInteger(1),
                ["totalPages"] = new OpenApiInteger(1)
            };

            schema.Example = paged;
        }
    }
}
