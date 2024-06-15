using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DotaWordle.DataAcess.Postgres;

public class HeroesDbContextFactory : IDesignTimeDbContextFactory<HeroesDbContext>
{
    public HeroesDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HeroesDbContext>();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddUserSecrets("e9b1d9a8-f9d7-47fa-a562-373df830c08b")
            .Build();

        var connectionString = configuration.GetConnectionString("PostgresConnection");
        optionsBuilder.UseNpgsql(connectionString);

        return new HeroesDbContext(optionsBuilder.Options);
    }
}
