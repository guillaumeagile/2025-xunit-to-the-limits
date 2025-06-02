using System.Text.Json;
using Ardalis.GuardClauses;
using Microsoft.OpenApi;
using Microsoft.AspNetCore.OpenApi;
using Scalar.AspNetCore;
using T8_Repositories_Adapters.source;

namespace T9webAPI;

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

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            // This will make the OpenAPI document available at /openapi/v1.json
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseAuthorization();

        app.MapGet("/weather", WeatherHandler)
            .WithName("GetWeatherForecast")
            .WithOpenApi();

        app.MapGet("/stored/{id}", GetById)
            .WithName("GetById")
            .WithOpenApi();


        app.Run();

        async Task GetById(HttpContext context, string id)
        {
            var storageAdapter = app.Services.GetService<IStorageAdapter<SomeDto>>();
            Guard.Against.Null(context, nameof(context));
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