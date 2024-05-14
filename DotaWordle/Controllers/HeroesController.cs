using AutoMapper;
using Dota_Wordle.Logic;
using DotaWordle.DataAcess.Postgres;
using DotaWordle.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotaWordle.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HeroesController(HeroesDbContext context, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Hero>> Get()
    {
        var heroes = context.Heroes
            .AsNoTracking()
            .Include(hero => hero.Roles)
            .Include(hero => hero.WeekWinrates)
            .Select(heroEntity => mapper.Map<Hero>(heroEntity));

        return Ok(heroes);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Hero> Get(int id)
    {
        var hero = context.Heroes
            .AsNoTracking()
            .Where(hero => hero.Id == id)
            .Include(hero => hero.Roles)
            .Include(hero => hero.WeekWinrates)
            .FirstOrDefault();

        if (hero == null)
            return NotFound(new { message = "Hero not found" });

        return Ok(mapper.Map<Hero>(hero));
    }

    [HttpGet("compare/{heroId:int}/{comparedHeroId:int}")]
    public ActionResult<HeroComparison> CompareHeroes(int heroId, int comparedHeroId)
    {
        var heroes = context.Heroes
            .AsNoTracking()
            .Where(hero => hero.Id == heroId || hero.Id == comparedHeroId)
            .Include(hero => hero.Roles)
            .Include(hero => hero.WeekWinrates);

        var firstHeroEntity = heroes.FirstOrDefault(hero => hero.Id == heroId);
        var secondHeroEntity = heroes.FirstOrDefault(hero => hero.Id == comparedHeroId);

        if (firstHeroEntity == null)
            return NotFound(new { message = "First hero not found" });
        if (secondHeroEntity == null)
            return NotFound(new { message = "Second hero not found" });

        var firstHero = mapper.Map<Hero>(firstHeroEntity);
        var secondHero = mapper.Map<Hero>(secondHeroEntity);

        var heroComparer = new HeroParametersComparer();

        return Ok(heroComparer.CompareHeroes(firstHero, secondHero));
    }
}