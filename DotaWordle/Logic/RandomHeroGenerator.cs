namespace Dota_Wordle.Logic;

public class RandomHeroGenerator(IHeroRepository heroRepository) : IRandomHeroGenerator
{
    private static readonly Random random = new();
    
    public int GetRandomHero()
    {
        var heroIds = heroRepository.GetHeroes().Select(hero => hero.Id).OrderBy(id => id).ToList();
        var randomId = random.Next(heroIds.Count);
        
        return heroIds[randomId];
    }
}