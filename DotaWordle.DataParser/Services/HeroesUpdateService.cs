using AutoMapper;
using DotaWordle.DataAcess.Postgres;
using Microsoft.EntityFrameworkCore;

namespace DataParser;

public class HeroesUpdateService(
    IServiceProvider serviceProvider,
    ILogger<HeroesUpdateService> logger,
    IMapper mapper)
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
        
        //TODO: Написать красивое решение на случай если дропнется одна из бд
        // var currentHeroes = await context.Heroes.ToListAsync();
        //
        // foreach (var hero in heroesList)
        // {
        //     var existingHero = currentHeroes.FirstOrDefault(h => h.Id == hero.Id);
        //     if (existingHero is null)
        //     {
        //         context.Heroes.Add(hero);
        //     }
        //     else
        //     {
        //         mapper.Map(hero, existingHero);
        //         context.Update(existingHero);
        //     }
        // }

        context.Heroes.UpdateRange(heroesList);
        await context.SaveChangesAsync();

        logger.LogInformation("Heroes data updated at {Time}", DateTimeOffset.Now);
    }
}