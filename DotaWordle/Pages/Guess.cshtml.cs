using AutoMapper;
using Dota_Wordle.Logic;
using DotaWordle.DataAcess.Postgres;
using DotaWordle.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DotaWordle.Pages;

public class Guess(IHeroParametersComparer heroComparer, IHeroRepository heroRepository) : PageModel
{
    public Hero HiddenHero { get; set; }

    public List<Hero> Heroes { get; set; }

    public IActionResult OnGet()
    {
        if (!HttpContext.Session.IsAvailable)
            return BadRequest(400);

        var hiddenHeroNumber = HttpContext.Session.GetInt32("hiddenHeroId");

        if (hiddenHeroNumber == null)
            return StatusCode(500);

        Heroes = heroRepository.GetHeroes().OrderBy(x => x.Name).ToList();

        HiddenHero = heroRepository.GetHeroById(hiddenHeroNumber.Value);

        return Page();
    }
}