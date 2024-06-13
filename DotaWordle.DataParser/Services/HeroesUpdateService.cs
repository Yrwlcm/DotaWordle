using DotaWordle.DataAcess.Postgres;

namespace DataParser;

public class HeroesUpdateService(
    IServiceProvider serviceProvider,
    ILogger<HeroesUpdateService> logger)
    : BackgroundService
{
    private readonly PeriodicTimer timer = new(TimeSpan.FromHours(24));

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            await UpdateHeroesData();
        }
    }

    private async Task UpdateHeroesData()
    {
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<HeroesDbContext>();
        var heroesStatisicsParser = scope.ServiceProvider.GetRequiredService<IHeroesStatisicsParser>();

        var heroesList = await heroesStatisicsParser.ParseAllHeroesAsync();

        context.Heroes.UpdateRange(heroesList);
        await context.SaveChangesAsync();

        logger.LogInformation("Heroes data updated at {Time}", DateTimeOffset.Now);
    }
}