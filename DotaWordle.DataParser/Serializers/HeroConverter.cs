using DotaWordle.DataAcess.Postgres.Enums;
using DotaWordle.DataAcess.Postgres.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataParser.Serializers;

public class HeroConverter : JsonConverter<HeroEntity>
{
    private readonly Dictionary<string, PrimaryAttribute> attributesAbbreviations = new()
    {
        { "str", PrimaryAttribute.Strength },
        { "agi", PrimaryAttribute.Agility },
        { "int", PrimaryAttribute.Intelligence },
        { "all", PrimaryAttribute.All },
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
            Roles = jobject["roles"].ToObject<List<RoleEntity>>(serializer),
            AttackType = heroStats.Value<string>("attackType"),
            StartingArmor = heroStats.Value<float>("startingArmor"),
            StartingDamageMin = heroStats.Value<float>("startingDamageMin"),
            StartingDamageMax = heroStats.Value<float>("startingDamageMax"),
            StartingMovespeed = heroStats.Value<float>("moveSpeed"),
            AttackRange = heroStats.Value<float>("attackRange"),
            PrimaryAttributeId = attributesAbbreviations[heroStats.Value<string>("primaryAttribute")],
            StrengthBase = heroStats.Value<float>("strengthBase"),
            AgilityBase = heroStats.Value<float>("agilityBase"),
            IntelligenceBase = heroStats.Value<float>("intelligenceBase"),
            Complexity = heroStats.Value<byte>("complexity")
        };

        heroEnitiy.Roles = heroEnitiy.Roles.Select(role => role with { HeroId = heroEnitiy.Id }).ToList();

        if (heroEnitiy.PrimaryAttributeId == PrimaryAttribute.All)
        {
            var allAttributesSum = heroEnitiy.StrengthBase + heroEnitiy.AgilityBase + heroEnitiy.IntelligenceBase;
            heroEnitiy.StartingDamageMin = float.Round(heroEnitiy.StartingDamageMin + allAttributesSum * 0.7f );
            heroEnitiy.StartingDamageMax = float.Round(heroEnitiy.StartingDamageMax + allAttributesSum * 0.7f );
        }
        
        return heroEnitiy;
    }
}