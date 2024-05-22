using Dota_Wordle.Logic;
using DotaWordle.Controllers;
using DotaWordle.DataAcess.Postgres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotaWordle.Pages;

public class Index(IRandomHeroGenerator randomHeroGenerator) : PageModel
{
    public IActionResult OnPostChooseHeroIdAndRedirect()
    {
        var heroId = randomHeroGenerator.GetRandomHero();
        
        HttpContext.Session.SetInt32("hiddenHeroId", heroId);
        
        return RedirectToPage("Guess");
    }
}