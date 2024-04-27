using DotaWordle.DataAcess.Postgres.Enums;

namespace DotaWordle.DataAcess.Postgres.Models;

public class RoleTypeEntity
{
    public RoleType RoleTypeId { get; set; }
    public string Name { get; set; }
    
    public List<RoleEntity> Roles { get; set; } = new();
}