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
}