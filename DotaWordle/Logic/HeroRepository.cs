using AutoMapper;
using DotaWordle.DataAcess.Postgres;
using DotaWordle.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Dota_Wordle.Logic;

public class HeroRepository(HeroesDbContext context, IMapper mapper, IMemoryCache cache) : IHeroRepository
{
    public List<Hero> GetHeroes()
    {
        const string cacheKey = "allHeroesList";
        
        if (cache.TryGetValue(cacheKey, out List<Hero> heroes)) 
            return heroes;
        
        heroes = context.Heroes
            .Include(hero => hero.Roles)
            .Include(hero => hero.WeekWinrates)
            .Select(hero => mapper.Map<Hero>(hero))
            .AsEnumerable()
            .ToList();

        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(30));

        cache.Set(cacheKey, heroes, cacheEntryOptions);
        
        return heroes;
    }
    
    public Hero? GetHeroById(int id)
    {
        var heroes = GetHeroes();
        
        return heroes.FirstOrDefault(hero => hero.Id == id);
    }
}