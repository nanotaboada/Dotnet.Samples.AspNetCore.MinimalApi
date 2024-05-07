using Dotnet.Samples.AspNetCore.MinimalApi;
using Microsoft.EntityFrameworkCore;

/* -----------------------------------------------------------------------------
 * Services
 * https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis#add-services
 * -------------------------------------------------------------------------- */

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PlayerDbContext>(options => options.UseInMemoryDatabase("Players"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(settings =>
{
    settings.DocumentName = "players-api";
    settings.Title = "Players (Minimal API)";
    settings.Version = "v1";
    settings.Description =
        "Proof of Concept for a Minimal API made with .NET 8 (LTS) and ASP.NET Core 8.0";
});

/* -----------------------------------------------------------------------------
 * Middlewares
 * https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis#aspnet-core-middleware
 * -------------------------------------------------------------------------- */

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(settings =>
    {
        settings.DocumentTitle = "Players (Minimal API)";
        settings.Path = "/swagger";
        settings.DocumentPath = "/swagger/{documentName}/swagger.json";
        settings.DocExpansion = "list";
    });
}

PlayerRoutes.Map(app);

/* -----------------------------------------------------------------------------
 * Data Seeding
 * https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding
 * -------------------------------------------------------------------------- */

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<PlayerDbContext>();
dbContext.Seed();

app.Run();
