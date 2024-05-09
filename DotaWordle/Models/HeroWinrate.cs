namespace DotaWordle.Models;

public record HeroWinrate
{
    public string RankBracket { get; set; }
    public int Winrate { get; set; }
}