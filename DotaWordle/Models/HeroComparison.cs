namespace DotaWordle.Models;

public record HeroComparison
{
    public string HeroName { get; set; }
    public string ComparedHeroName { get; set; }
    public bool SameAttackType { get; set; }
    public int ArmorBaseComparision { get; set; }
    public int DamageMinBaseComparision { get; set; }
    public int DamageMaxBaseComparision { get; set; }
    public int MoveSpeedBaseComparision { get; set; }
    public int AttackRangeComparision { get; set; }
    public int StrengthBaseComparision { get; set; }
    public int AgilityBaseComparision { get; set; }
    public int IntelligenceBaseComparision { get; set; }
    public int ComplexityComparision { get; set; }
    public bool SamePrimaryAttributeName { get; set; }
    public List<RoleComparision>? RolesComparision { get; set; }
    public List<HeroWinrateComparision>? WeekWinratesComparision { get; set; }
}