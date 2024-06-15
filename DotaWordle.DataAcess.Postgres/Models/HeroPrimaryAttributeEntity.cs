using DotaWordle.DataAcess.Postgres.Enums;

namespace DotaWordle.DataAcess.Postgres.Models;

public class HeroPrimaryAttributeEntity
{
    public HeroPrimaryAttribute HeroPrimaryAttributeId { get; set; }
    public string Name { get; set; }

    public List<HeroEntity>? Heroes { get; set; } = new();
}