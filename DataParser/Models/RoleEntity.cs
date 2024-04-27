using DataParser.Enums;
using Newtonsoft.Json;

namespace DataParser.Models;

public record RoleEntity
{
    public int Id { get; set; } = new();
    public int HeroId { get; set; }
    public HeroEntity? Hero { get; set; }
    [JsonProperty("RoleId")]
    public RoleType RoleTypeId { get; set; }
    public RoleTypeEntity? RoleType { get; set; }
    public short Level { get; set; }
}