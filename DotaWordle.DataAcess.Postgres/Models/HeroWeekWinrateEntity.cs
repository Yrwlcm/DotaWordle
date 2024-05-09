using DotaWordle.DataAcess.Postgres.Enums;

namespace DotaWordle.DataAcess.Postgres.Models;

public record HeroWeekWinrateEntity
{
    public int HeroId { get; set; }
    public HeroEntity? Hero { get; set; }
    public RankBracket RankBracketId { get; set; }
    public RankBracketEntity? RankBracket { get; set; }
    public long Wins { get; set; }
    public long Matches { get; set; }
}