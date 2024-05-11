namespace DotaWordle.Models;

public record HeroWinrate : IComparable<HeroWinrate>
{
    public string RankBracket { get; set; }
    public int Winrate { get; set; }

    public int CompareTo(HeroWinrate? other)
    {
        if (ReferenceEquals(this, other)) 
            return 0;
        
        if (ReferenceEquals(null, other)) 
            return 1;
        
        if (!string.Equals(RankBracket, other.RankBracket))
            throw new ArgumentException("Cannot compare different rank brackets");
        
        return Winrate.CompareTo(other.Winrate);
    }
}