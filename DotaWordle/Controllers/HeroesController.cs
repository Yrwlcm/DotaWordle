using AutoMapper;
using Dota_Wordle.Logic;
using DotaWordle.DataAcess.Postgres;
using DotaWordle.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotaWordle.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HeroesController(IHeroParametersComparer comparer, IHeroRepository heroRepository)
    : ControllerBase
{
    private static readonly Random random = new();

    [HttpGet]
    public ActionResult<IEnumerable<Hero>> Get()
    {
        return Ok(heroRepository.GetHeroes());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Hero> Get(int id)
    {
        var hero = heroRepository.GetHeroById(id);
        if (hero == null)
            return NotFound(new { message = "Hero not found" });

        return Ok(hero);
    }

    [HttpGet("compare/{heroId:int}/{comparedHeroId:int}")]
    public ActionResult<HeroComparison> CompareHeroes(int heroId, int comparedHeroId)
    {
        var firstHero = heroRepository.GetHeroById(heroId);
        var secondHero = heroRepository.GetHeroById(comparedHeroId);

        if (firstHero == null)
            return NotFound(new { message = "First hero not found" });
        if (secondHero == null)
            return NotFound(new { message = "Second hero not found" });

        return Ok(comparer.CompareHeroes(firstHero, secondHero));
    }

    [HttpPost("generate/hiddenhero")]
    public ActionResult GenerateHiddenHero()
    {
        var heroIds = heroRepository.GetHeroes().Select(hero => hero.Id).OrderBy(id => id).ToList();
        var randomId = random.Next(heroIds.Count);
        var heroId = heroIds[randomId];

        HttpContext.Session.SetInt32("hiddenHeroId", heroId);

        return Ok(new { hiddenHeroId = heroId });
    }

    [HttpGet("compare/hiddenHero/{heroId:int}")]
    public ActionResult<HeroComparison> CompareToHiddenHero(int heroId)
    {
        var hiddenHeroId = HttpContext.Session.GetInt32("hiddenHeroId");

        if (hiddenHeroId == null)
            return NotFound(new { message = "Hidden was not generated" });

        var hiddenHero = heroRepository.GetHeroById(hiddenHeroId.Value);
        var hero = heroRepository.GetHeroById(heroId);

        return Ok(new { hero, comparision = comparer.CompareHeroes(hiddenHero, hero) });
    }
}