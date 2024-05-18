using DotaWordle.Models;

namespace Dota_Wordle.Logic;

public interface IHeroRepository
{
    public List<Hero> GetHeroes();
    public Hero? GetHeroById(int id);
}