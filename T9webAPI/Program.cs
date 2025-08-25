using System.Text.Json;
using Ardalis.GuardClauses;
using Microsoft.OpenApi;
using Microsoft.AspNetCore.OpenApi;

using T8_Repositories_Adapters.source;

namespace T9webAPI;

// Mock implementation for testing purposes

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Add minimal API explorer and OpenAPI support
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi("v1", options =>
        {
            // Specify the OpenAPI version to use
            options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;
        });
        
        // Register storage adapter (using a mock implementation for now)
        builder.Services.AddSingleton<IStorageAdapter<SomeDto>, MockStorageAdapter>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            // This will make the OpenAPI document available at /openapi/v1.json
            app.MapOpenApi();
            
            // Add a simple endpoint to view the API documentation
            app.MapGet("/", () => Results.Content("""
                <!DOCTYPE html>
                <html>
                <head>
                    <title>T9webAPI Documentation</title>
                    <style>
                        body { font-family: Arial, sans-serif; margin: 40px; }
                        .endpoint { background: #f5f5f5; padding: 10px; margin: 10px 0; border-radius: 5px; }
                        .method { font-weight: bold; color: #2196F3; }
                        .swagger-link { background: #4CAF50; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px; display: inline-block; margin: 10px 0; }
                        .swagger-link:hover { background: #45a049; }
                    </style>
                </head>
                <body>
                    <h1>T9webAPI - .NET 10 Web API</h1>
                    
                    <h2>Interactive API Documentation:</h2>
                    <a href="/swagger/" class="swagger-link"> (broken) Open Swagger UI</a>
                    
                    <h2>Available Endpoints:</h2>
                    
                    <div class="endpoint">
                        <span class="method">GET</span> <strong>/weather</strong><br>
                        <em>Get weather forecast</em><br>
                        Returns a 5-day weather forecast with random data
                    </div>
                    
                    <div class="endpoint">
                        <span class="method">GET</span> <strong>/stored/{id}</strong><br>
                        <em>Get stored item by ID</em><br>
                        Retrieves a stored item by its ID from the mock storage (try IDs: 1, 2, or 3)
                    </div>
                    
                    <h2>OpenAPI Specification:</h2>
                    <p><a href="/openapi/v1.json" target="_blank">View OpenAPI JSON</a></p>
                    
                    <h2>Try the API:</h2>
                    <ul>
                        <li><a href="/weather" target="_blank">GET /weather</a></li>
                        <li><a href="/stored/1" target="_blank">GET /stored/1</a></li>
                        <li><a href="/stored/2" target="_blank">GET /stored/2</a></li>
                        <li><a href="/stored/3" target="_blank">GET /stored/3</a></li>
                    </ul>
                </body>
                </html>
                """, "text/html"));
                
            // Add Swagger UI endpoint that consumes the native OpenAPI JSON
            app.MapGet("/swagger", () => Results.Content("""
                <!DOCTYPE html>
                <html lang="en">
                <head>
                    <meta charset="UTF-8">
                    <meta name="viewport" content="width=device-width, initial-scale=1.0">
                    <title>T9webAPI - Swagger UI</title>
                    <link rel="stylesheet" type="text/css" href="https://unpkg.com/swagger-ui-dist@5.10.3/swagger-ui.css" />
                    <style>
                        html { box-sizing: border-box; overflow: -moz-scrollbars-vertical; overflow-y: scroll; }
                        *, *:before, *:after { box-sizing: inherit; }
                        body { margin:0; background: #fafafa; }
                    </style>
                </head>
                <body>
                    <div id="swagger-ui"></div>
                    <script src="https://unpkg.com/swagger-ui-dist@5.10.3/swagger-ui-bundle.js"></script>
                    <script src="https://unpkg.com/swagger-ui-dist@5.10.3/swagger-ui-standalone-preset.js"></script>
                    <script>
                        window.onload = function() {
                            const ui = SwaggerUIBundle({
                                url: '/openapi/v1.json',
                                dom_id: '#swagger-ui',
                                deepLinking: true,
                                presets: [
                                    SwaggerUIBundle.presets.apis,
                                    SwaggerUIStandalonePreset
                                ],
                                plugins: [
                                    SwaggerUIBundle.plugins.DownloadUrl
                                ],
                                layout: "StandaloneLayout"
                            });
                        };
                    </script>
                </body>
                </html>
                """, "text/html"));
        }

        app.UseAuthorization();

        app.MapGet("/weather", WeatherHandler)
            .WithName("GetWeatherForecast")
            .WithSummary("Get weather forecast")
            .WithDescription("Returns a 5-day weather forecast with random data")
            .WithOpenApi();

        app.MapGet("/stored/{id}", GetById)
            .WithName("GetById")
            .WithSummary("Get stored item by ID")
            .WithDescription("Retrieves a stored item by its ID from the mock storage")
            .WithOpenApi();


        app.Run();

        async Task GetById(HttpContext context, string id)
        {
            var storageAdapter = app.Services.GetService<IStorageAdapter<SomeDto>>();
            Guard.Against.Null(context, nameof(context));
            Guard.Against.Null(storageAdapter, nameof(storageAdapter));
            //   var id = context.Request.RouteValues["id"];
            context.Response.ContentType = "application/json";

            var result = await storageAdapter.GetByIdAsync(id, CancellationToken.None);
            if (result.IsFailed)
            {
                context.Response.StatusCode = 404;
                return;
            }

            var dto = result.Value;
            var json = JsonSerializer.Serialize(dto);
            await context.Response.WriteAsync(json);
        }

        WeatherForecast[] WeatherHandler(HttpContext httpContext)
        {
            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };
            var forecast = Enumerable.Range(1, 5)
                .Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = summaries[Random.Shared.Next(summaries.Length)]
                })
                .ToArray();
            return forecast;
        }
    }
}