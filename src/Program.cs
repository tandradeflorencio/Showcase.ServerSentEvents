using Showcase.ServerSentEvents.Services;
using Showcase.ServerSentEvents.Services.Interfaces;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/v1/news", async (HttpContext httpContext, INewsService newsService, CancellationToken cancellationToken) =>
{
    httpContext.Response.Headers.Append("Content-Type", "text/event-stream");

    while (!cancellationToken.IsCancellationRequested)
    {
        var result = await newsService.GetAsync();

        await httpContext.Response.WriteAsync("data: ", cancellationToken: cancellationToken);
        await JsonSerializer.SerializeAsync(httpContext.Response.Body, result, cancellationToken: cancellationToken);
        await httpContext.Response.WriteAsync("\n\n", cancellationToken: cancellationToken);
        await httpContext.Response.Body.FlushAsync(cancellationToken: cancellationToken);
    }
})
.WithName("GetNews")
.WithOpenApi();

app.UseCors(builder => builder.AllowAnyOrigin());

app.Run();