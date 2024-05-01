using DotaWordle.DataAcess.Postgres.Enums;
using DotaWordle.DataAcess.Postgres.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataParser.Serializers;

public class HeroWinrateConverter(RankBracket bracket = RankBracket.Unknown) : JsonConverter<HeroWeekWinrateEntity>
{
    public override void WriteJson(JsonWriter writer, HeroWeekWinrateEntity? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override HeroWeekWinrateEntity? ReadJson(JsonReader reader, Type objectType, HeroWeekWinrateEntity? existingValue,
        bool hasExistingValue, JsonSerializer serializer)
    {
        var jobject = JObject.Load(reader);
        
        var heroWinrate = new HeroWeekWinrateEntity()
        {
            HeroId = jobject.Value<int>("heroId"),
            Wins = jobject.Value<int>("winCount"),
            Matches = jobject.Value<int>("matchCount"),
            RankBracketId = bracket
        };
        
        return heroWinrate;
    }
}