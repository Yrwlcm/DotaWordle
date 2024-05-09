namespace DotaWordle.Models;

public record Hero
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string AttackType { get; set; }
    public float StartingArmor { get; set; }
    public float StartingDamageMin { get; set; }
    public float StartingDamageMax { get; set; }
    public float StartingMovespeed { get; set; }
    public float AttackRange { get; set; }
    public float StrengthBase { get; set; }
    public float AgilityBase { get; set; }
    public float IntelligenceBase { get; set; }
    public byte Complexity { get; set; }
    public string PrimaryAttributeName { get; set; }
    public List<Role>? Roles { get; set; }
    public List<HeroWinrate>? WeekWinrates { get; set; }
}