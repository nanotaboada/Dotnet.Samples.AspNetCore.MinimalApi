/* -----------------------------------------------------------------------------
 * Route Handlers
 * https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis#route-handlers
 * -------------------------------------------------------------------------- */

namespace Dotnet.Samples.AspNetCore.MinimalApi;

using Microsoft.EntityFrameworkCore;

public static class PlayerRouteHandlers
{
    // Retrieve ----------------------------------------------------------------

    public static async Task<IResult> RetrievePlayers(PlayerDbContext db)
    {
        return TypedResults.Ok(await db.Players.ToArrayAsync());
    }

    public static async Task<IResult> RetrievePlayerById(int id, PlayerDbContext db)
    {
        if (await db.Players.FindAsync(id) is Player entity)
        {
            return TypedResults.Ok(entity);
        }

        return TypedResults.NotFound();
    }

    public static async Task<IResult> RetrievePlayerBySquadNumber(
        int squadNumber,
        PlayerDbContext db
    )
    {
        if (
            await db
                .Players.Where(player => player.SquadNumber == squadNumber)
                .SingleOrDefaultAsync()
            is Player entity
        )
        {
            return TypedResults.Ok(entity);
        }

        return TypedResults.NotFound();
    }

    // Create ------------------------------------------------------------------

    public static async Task<IResult> CreatePlayer(Player player, PlayerDbContext db)
    {
        if (await db.Players.FindAsync(player.Id) is Player entity)
        {
            return TypedResults.Conflict(entity);
        }

        db.Players.Add(player);
        await db.SaveChangesAsync();

        return TypedResults.Created($"/players/{player.Id}", player);
    }

    // Update ------------------------------------------------------------------

    public static async Task<IResult> UpdatePlayer(int id, Player player, PlayerDbContext db)
    {
        if (await db.Players.FindAsync(id) is Player entity)
        {
            entity.MapFrom(player);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        return TypedResults.NotFound();
    }

    // Delete ------------------------------------------------------------------

    public static async Task<IResult> DeletePlayer(int id, PlayerDbContext db)
    {
        if (await db.Players.FindAsync(id) is Player entity)
        {
            db.Players.Remove(entity);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        return TypedResults.NotFound();
    }
}
