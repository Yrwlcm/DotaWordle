using DotaWordle.DataAcess.Postgres.Enums;
using DotaWordle.DataAcess.Postgres.Models;

namespace DataParser;

public interface IHeroesStatisicsParser
{
    Task<List<HeroEntity>> ParseAllHeroesAsync();
    Task<List<List<HeroWeekWinrateEntity>>> ParseAllHeroesWeekWinratesAsync(RankBracket[] rankBrackets);
    Task<List<HeroWeekWinrateEntity>> ParseAllHeroesWeekWinratesAsync(RankBracket rankBracket);
}