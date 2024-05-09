using DotaWordle.DataAcess.Postgres.Enums;

namespace DotaWordle.DataAcess.Postgres.Models;

public class RankBracketEntity
{
    public RankBracket RankBracketId { get; set; }
    public string Name { get; set; }

    public List<HeroWeekWinrateEntity>? WeekWinrates { get; set; } = [];
}