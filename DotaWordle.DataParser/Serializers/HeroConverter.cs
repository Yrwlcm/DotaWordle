using DotaWordle.DataAcess.Postgres.Enums;
using DotaWordle.DataAcess.Postgres.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataParser.Serializers;

public class HeroConverter : JsonConverter<HeroEntity>
{
    private readonly Dictionary<string, HeroPrimaryAttribute> attributesAbbreviations = new()
    {
        { "str", HeroPrimaryAttribute.Strength },
        { "agi", HeroPrimaryAttribute.Agility },
        { "int", HeroPrimaryAttribute.Intelligence },
        { "all", HeroPrimaryAttribute.All },
    };

    public override void WriteJson(JsonWriter writer, HeroEntity? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override HeroEntity? ReadJson(JsonReader reader, Type objectType, HeroEntity? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        var jobject = JObject.Load(reader);
        var heroStats = jobject.Value<JObject>("stats");
        
        var heroEnitiy = new HeroEntity
        {
            Id = jobject.Value<int>("id"),
            Name = jobject.Value<string>("displayName"),
            GameVersion = jobject.Value<int>("gameVersionId"),
            Roles = jobject["roles"].ToObject<List<HeroRoleEntity>>(serializer),
            AttackType = heroStats.Value<string>("attackType"),
            ArmorBase = heroStats.Value<float>("startingArmor"),
            DamageMinBase = heroStats.Value<float>("startingDamageMin"),
            DamageMaxBase = heroStats.Value<float>("startingDamageMax"),
            MoveSpeedBase = heroStats.Value<float>("moveSpeed"),
            AttackRange = heroStats.Value<float>("attackRange"),
            PrimaryAttributeId = attributesAbbreviations[heroStats.Value<string>("primaryAttribute")],
            StrengthBase = heroStats.Value<float>("strengthBase"),
            AgilityBase = heroStats.Value<float>("agilityBase"),
            IntelligenceBase = heroStats.Value<float>("intelligenceBase"),
            Complexity = heroStats.Value<byte>("complexity")
        };

        heroEnitiy.Roles = heroEnitiy.Roles.Select(role => role with { HeroId = heroEnitiy.Id }).ToList();

        if (heroEnitiy.PrimaryAttributeId == HeroPrimaryAttribute.All)
        {
            var allAttributesSum = heroEnitiy.StrengthBase + heroEnitiy.AgilityBase + heroEnitiy.IntelligenceBase;
            heroEnitiy.DamageMinBase = float.Round(heroEnitiy.DamageMinBase + allAttributesSum * 0.7f );
            heroEnitiy.DamageMaxBase = float.Round(heroEnitiy.DamageMaxBase + allAttributesSum * 0.7f );
        }
        
        return heroEnitiy;
    }
}