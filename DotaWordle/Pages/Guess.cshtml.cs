using AutoMapper;
using DotaWordle.DataAcess.Postgres;
using DotaWordle.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DotaWordle.Pages;

public class Guess(HeroesDbContext context, IMapper mapper) : PageModel
{
    public int? HiddenHeroNumber { get; set; }
    public Hero Hero { get; set; }

    public IActionResult OnGet()
    {
        if (!HttpContext.Session.IsAvailable)
            return BadRequest(400);
        
        HiddenHeroNumber = HttpContext.Session.GetInt32("hiddenHeroId");
        
        if (HiddenHeroNumber == null)
            return StatusCode(500);

        var heroEntity = context.Heroes
            .Where(x => x.Id == HiddenHeroNumber)
            .Include(h => h.Roles)
            .Include(h => h.WeekWinrates)
            .FirstOrDefault();
        
        Hero = mapper.Map<Hero>(heroEntity);
        
        return Page();
    }
}