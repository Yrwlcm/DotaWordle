using DataParser.Serializers;
using DotaWordle.DataAcess.Postgres.Enums;
using DotaWordle.DataAcess.Postgres.Models;
using FluentAssertions;
using Newtonsoft.Json;

namespace DataParserTests;

public class HeroWinrateConverterTests
{
    private string json;

    [SetUp]
    public void Setup()
    {
        json = """
               {
                   "heroId": 1,
                   "winCount": 108768,
                   "matchCount": 208177
               }
               """;
    }

    [Test]
    public void ShouldDeserializeCorrectJson()
    {
        var expectedHeroWinrate = new HeroWeekWinrateEntity
        {
            HeroId = 1,
            Wins = 108768,
            Matches = 208177,
            RankBracketId = RankBracket.Unknown
        };

        var deserializedHeroWinrate = JsonConvert.DeserializeObject<HeroWeekWinrateEntity>(json, new HeroWinrateConverter());
        deserializedHeroWinrate.Should().BeEquivalentTo(expectedHeroWinrate, o => o.Excluding(p => p.Hero));
    }
}