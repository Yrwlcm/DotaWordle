using DotaWordle.Models;

namespace Dota_Wordle.Logic;

public interface IHeroParametersComparer
{
    public HeroComparison CompareHeroes(Hero hero, Hero comparedHero);
}