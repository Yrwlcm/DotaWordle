using DotaWordle.DataAcess.Postgres.Enums;
using Newtonsoft.Json;

namespace DotaWordle.DataAcess.Postgres.Models;

public record HeroRoleEntity
{
    public int HeroId { get; set; }
    public HeroEntity? Hero { get; set; }
    [JsonProperty("RoleId")]
    public HeroRoleType HeroRoleTypeId { get; set; }
    public HeroRoleTypeEntity? RoleType { get; set; }
    public short Level { get; set; }
}