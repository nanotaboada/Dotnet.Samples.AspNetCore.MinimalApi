namespace Dotnet.Samples.AspNetCore.MinimalApi;

using Microsoft.EntityFrameworkCore;

public class PlayerDbContext : DbContext
{
    public PlayerDbContext(DbContextOptions<PlayerDbContext> options)
        : base(options) { }

    public DbSet<Player> Players => Set<Player>();
}
