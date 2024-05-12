using DotaWordle.DataAcess.Postgres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotaWordle.Pages;

[IgnoreAntiforgeryToken]
public class Index(HeroesDbContext context) : PageModel
{
    private static readonly Random Random = new();
    
    public IActionResult OnPostChooseHeroIdAndRedirect()
    {
        var heroIds = context.Heroes.Select(x => x.Id).ToList();
        var randomId = Random.Next(heroIds.Count);
        var heroId = heroIds[randomId];
        
        HttpContext.Session.SetInt32("hiddenHeroId", heroId);
        
        return RedirectToPage("Guess");
    }
}