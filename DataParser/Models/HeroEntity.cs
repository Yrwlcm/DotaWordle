using Attribute = DataParser.Enums.Attribute;

namespace DataParser.Models;

public class HeroEntity
{
    public Guid Id { get; set; }
    public int HeroId { get; set; }
    public string Name { get; set; }
    public int GameVersion { get; set; }
    public List<RoleEntity> Roles { get; set; }
    public string AttackType { get; set; }
    public float StartingArmor { get; set; }
    public float StartingDamageMin { get; set; }
    public float StartingDamageMax { get; set; }
    public float StartingMovespeed { get; set; }
    public float AttackRange { get; set; }
    public Attribute PrimaryAttribute { get; set; }
    public float StrengthBase { get; set; }
    public float AgilityBase { get; set; }
    public float IntelligenceBase { get; set; }
    public byte Complexity { get; set; }
}

