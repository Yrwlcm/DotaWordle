using DataParser.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Attribute = DataParser.Enums.Attribute;

namespace DataParser.Serializers;

public class HeroConverter : JsonConverter<HeroEntity>
{
    private readonly Dictionary<string, Attribute> attributesAbbreviations = new()
    {
        { "str", Attribute.Strength },
        { "agi", Attribute.Agility },
        { "int", Attribute.Intelligence },
        { "all", Attribute.All },
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
            HeroId = jobject.Value<int>("id"),
            Name = jobject.Value<string>("displayName"),
            GameVersion = jobject.Value<int>("gameVersionId"),
            Roles = jobject["roles"].ToObject<List<RoleEntity>>(serializer),
            AttackType = heroStats.Value<string>("attackType"),
            StartingArmor = heroStats.Value<float>("startingArmor"),
            StartingDamageMin = heroStats.Value<float>("startingDamageMin"),
            StartingDamageMax = heroStats.Value<float>("startingDamageMax"),
            StartingMovespeed = heroStats.Value<float>("moveSpeed"),
            AttackRange = heroStats.Value<float>("attackRange"),
            PrimaryAttribute = attributesAbbreviations[heroStats.Value<string>("primaryAttribute")],
            StrengthBase = heroStats.Value<float>("strengthBase"),
            AgilityBase = heroStats.Value<float>("agilityBase"),
            IntelligenceBase = heroStats.Value<float>("intelligenceBase"),
            Complexity = heroStats.Value<byte>("complexity")
        };
        return heroEnitiy;
    }
}