using System.Net.Http.Json;
using DataParser.Serializers;
using DotaWordle.DataAcess.Postgres.Enums;
using DotaWordle.DataAcess.Postgres.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataParser;

public class StratzApiParser : IDisposable, IHeroesStatisicsParser
{
    private readonly HttpClient Client = new();

    public async Task<List<HeroEntity>> ParseAllHeroesAsync()
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

        var responce = await SendStratzGraphQLRequest(query);
        var jsonString = await responce.Content.ReadAsStringAsync();

        var responseTokens = JObject.Parse(jsonString);
        var heroesTokens = responseTokens["data"]["constants"]["heroes"].Children().ToList();

        var heroesList = new List<HeroEntity>();

        foreach (var heroToken in heroesTokens)
        {
            var heroEntity = JsonConvert.DeserializeObject<HeroEntity>(heroToken.ToString(), new HeroConverter());
            heroesList.Add(heroEntity);
        }

        return heroesList;
    }

    public async Task<List<List<HeroWeekWinrateEntity>>> ParseAllHeroesWeekWinratesAsync(RankBracket[] rankBrackets)
    {
        var winrates = new List<List<HeroWeekWinrateEntity>>();

        foreach (var rankBracket in rankBrackets)
        {
            winrates.Add(await ParseAllHeroesWeekWinratesAsync(rankBracket));
        }

        return winrates;
    }

    public async Task<List<HeroWeekWinrateEntity>> ParseAllHeroesWeekWinratesAsync(RankBracket rankBracket)
    {
        var rankSearch = rankBracket.ToString().ToUpperInvariant();
        rankSearch = rankSearch.Equals(RankBracket.All.ToString(), StringComparison.InvariantCultureIgnoreCase)
            ? ""
            : rankSearch;

        var query = $$"""
                      {
                         heroStats
                         {
                             winWeek(bracketIds:[{{rankSearch}}])
                             {
                                 week,
                                 heroId,
                                 winCount,
                                 matchCount
                             }
                         }
                      }
                      """;

        var responce = await SendStratzGraphQLRequest(query);
        var jsonString = await responce.Content.ReadAsStringAsync();

        var responseTokens = JObject.Parse(jsonString);
        var winratesTokens = responseTokens["data"]["heroStats"]["winWeek"].Children();

        var currentWeek = winratesTokens.FirstOrDefault()?["week"];
        var winratesForWeek = winratesTokens
            .Where(heroWinrateToken => heroWinrateToken["week"].Equals(currentWeek))
            .Select(heroWinrateToken =>
                JsonConvert.DeserializeObject<HeroWeekWinrateEntity>(heroWinrateToken.ToString(),
                    new HeroWinrateConverter(rankBracket)))
            .ToList();

        return winratesForWeek;
    }

    public async Task<HttpResponseMessage> SendStratzGraphQLRequest(string query)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, "https://api.stratz.com/graphql");
        request.Content = JsonContent.Create(new { query });
        request.Headers.Add("Authorization", $"Bearer {Environment.GetEnvironmentVariable("StratzApiKey")}");
        var response = await Client.SendAsync(request);
        return response;
    }

    public void Dispose()
    {
        Client.Dispose();
        GC.SuppressFinalize(this);
    }
}