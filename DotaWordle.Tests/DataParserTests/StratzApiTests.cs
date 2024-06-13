using DataParser;
using DotaWordle.DataAcess.Postgres.Enums;
using FluentAssertions;

namespace DataParserTests;

[TestFixture]
public class StratzApiTests
{
    private StratzApiParser parser;

    [SetUp]
    public void Setup()
    {
        parser = new StratzApiParser();
    }

    [TearDown]
    public void TearDown()
    {
        parser.Dispose();
    }

    [Test]
    public Task GetCorrectHeroesResponse()
    {
        const string query = """
                             {
                               constants {
                                 heroes {
                                   id
                                   displayName
                                   gameVersionId
                                   roles {
                                     roleId
                                     level
                                   }
                                   stats {
                                     attackType
                                     startingArmor
                                     startingDamageMin
                                     startingDamageMax
                                     attackRange
                                     primaryAttribute
                                     strengthBase
                                     agilityBase
                                     intelligenceBase
                                     moveSpeed
                                     complexity
                                   }
                                 }
                               }
                             }
                             """;

        var responce = parser.SendStratzGraphQLRequest(query).Result;

        return VerifyJson(responce.Content.ReadAsStringAsync());
    }

    [Test]
    public Task GetCorrectHeroesWinrateResponse()
    {
        const string query = """
                             {
                               heroStats {
                                 winWeek(bracketIds: [HERALD, LEGEND, IMMORTAL]) {
                                   heroId
                                   winCount
                                   matchCount
                                 }
                               }
                             }

                             """;

        var responce = parser.SendStratzGraphQLRequest(query).Result;
        return VerifyJson(responce.Content.ReadAsStringAsync());
    }

    [Test]
    public void GetCorrectHeroesWinratesForRankBracket()
    {
        var selectedBrackets = new[] { RankBracket.Herald, RankBracket.Legend, RankBracket.Immortal };

        var selectedBracketsMatches = parser.ParseAllHeroesWeekWinratesAsync(selectedBrackets).Result;
        var allBracketsMatches = parser.ParseAllHeroesWeekWinratesAsync([RankBracket.All]).Result;

        selectedBracketsMatches.Should().HaveCount(3);
        allBracketsMatches.Should().HaveCount(1);
    }
}