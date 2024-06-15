using DotaWordle.DataAcess.Postgres.Enums;

namespace DotaWordle.DataAcess.Postgres.Models;

public record HeroEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int GameVersion { get; set; }
    public List<HeroRoleEntity>? Roles { get; set; }
    public string AttackType { get; set; }
    public float ArmorBase { get; set; }
    public float DamageMinBase { get; set; }
    public float DamageMaxBase { get; set; }
    public float MoveSpeedBase { get; set; }
    public float AttackRange { get; set; }
    public HeroPrimaryAttribute PrimaryAttributeId { get; set; }
    public HeroPrimaryAttributeEntity? PrimaryAttribute { get; set; }
    public float StrengthBase { get; set; }
    public float AgilityBase { get; set; }
    public float IntelligenceBase { get; set; }
    public byte Complexity { get; set; }
    public List<HeroWeekWinrateEntity>? WeekWinrates { get; set; }
    
}

