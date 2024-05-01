using System.Net.Http.Json;
using DataParser;
using DotaWordle.DataAcess.Postgres.Enums;
using FluentAssertions;

namespace DataParserTests;

[TestFixture]
public class StratzApiTests
{
    private HttpClient client;

    [SetUp]
    public void Setup()
    {
        client = new HttpClient();
    }

    [TearDown]
    public void TearDown()
    {
        client.Dispose();
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

        var responce = StratzApi.SendStratzGraphQLRequest(client, query).Result;

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

        var responce = StratzApi.SendStratzGraphQLRequest(client, query).Result;
        return VerifyJson(responce.Content.ReadAsStringAsync());
    }

    [Test]
    public void GetCorrectHeroesWinratesForRankBracket()
    {
        var selectedBrackets = new[] { RankBracket.Herald, RankBracket.Legend, RankBracket.Immortal };

        var selectedBracketsMatches = StratzApi.ParseAllHeroesWeekWinrates(client, selectedBrackets).Result;
        var allBracketsMatches = StratzApi.ParseAllHeroesWeekWinrates(client, [RankBracket.All]).Result;

        selectedBracketsMatches.Should().HaveCount(3);
        allBracketsMatches.Should().HaveCount(1);
    }
}