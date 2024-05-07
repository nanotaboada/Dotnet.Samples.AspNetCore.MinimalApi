/* -----------------------------------------------------------------------------
 * Routes
 * https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis#routing
 * -------------------------------------------------------------------------- */

namespace Dotnet.Samples.AspNetCore.MinimalApi;

public static class PlayerRoutes
{
    public static void Map(WebApplication app)
    {
        var routes = app.MapGroup("/players");

        routes.MapGet("/", PlayerRouteHandlers.RetrievePlayers);
        routes.MapGet("/{id}", PlayerRouteHandlers.RetrievePlayerById);
        routes.MapGet("/squadNumber/{squadNumber}", PlayerRouteHandlers.RetrievePlayerBySquadNumber);
        routes.MapPost("/", PlayerRouteHandlers.CreatePlayer);
        routes.MapPut("/{id}", PlayerRouteHandlers.UpdatePlayer);
        routes.MapDelete("/{id}", PlayerRouteHandlers.DeletePlayer);
    }
}
