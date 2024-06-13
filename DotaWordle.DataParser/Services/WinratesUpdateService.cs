using DotaWordle.DataAcess.Postgres;
using DotaWordle.DataAcess.Postgres.Enums;

namespace DataParser;

public class WinratesUpdateService(
    IServiceProvider serviceProvider,
    ILogger<HeroesUpdateService> logger)
    : BackgroundService
{
    private readonly PeriodicTimer timer = new(TimeSpan.FromHours(6));

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            await UpdateHeroesWinrateData();
        }
    }

    private async Task UpdateHeroesWinrateData()
    {
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<HeroesDbContext>();
        var heroesStatisicsParser = scope.ServiceProvider.GetRequiredService<IHeroesStatisicsParser>();

        var weekWinrates = await heroesStatisicsParser
            .ParseAllHeroesWeekWinratesAsync([RankBracket.Herald, RankBracket.Legend, RankBracket.Immortal]);

        var flatWinrates = weekWinrates.SelectMany(x => x).ToList();

        context.HeroWeekWinrates.UpdateRange(flatWinrates);
        await context.SaveChangesAsync();

        logger.LogInformation("Heroes winrate data updated at {Time}", DateTimeOffset.Now);
    }
}