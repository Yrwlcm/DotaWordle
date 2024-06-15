using DotaWordle.DataAcess.Postgres.Enums;

namespace DotaWordle.DataAcess.Postgres.Models;

public class HeroRoleTypeEntity
{
    public HeroRoleType HeroRoleTypeId { get; set; }
    public string Name { get; set; }
    
    public List<HeroRoleEntity>? Roles { get; set; } = new();
}