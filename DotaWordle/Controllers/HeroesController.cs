using AutoMapper;
using DotaWordle.DataAcess.Postgres;
using DotaWordle.DataAcess.Postgres.Models;
using DotaWordle.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotaWordle.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HeroesController(HeroesDbContext context, IMapper mapper) : ControllerBase
{
    // GET: api/<HeroesController>
    [HttpGet]
    public IEnumerable<Hero> Get()
    {
        var heroes = context.Heroes
            .Include(hero => hero.Roles)
            .Include(hero => hero.WeekWinrates)
            .Select(heroEntity => mapper.Map<Hero>(heroEntity))
            .ToList();
        return heroes;
    }

    // GET api/<HeroesController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<HeroesController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<HeroesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<HeroesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}