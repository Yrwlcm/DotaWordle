using System.Net.Http.Json;
using DataParser.Models;
using DataParser.Serializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataParser;

public static class Program
{
    static async Task Main(string[] args)
    {
        var client = new HttpClient();
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

        using var request = new HttpRequestMessage(HttpMethod.Post, "https://api.stratz.com/graphql")
        {
            Content = JsonContent.Create(new { query })
        };
        request.Headers.Add("Authorization", $"Bearer {Environment.GetEnvironmentVariable("StratzApiKey")}");

        var responce = await client.SendAsync(request);
        var jsonString = await responce.Content.ReadAsStringAsync();
        var responseTokens = JObject.Parse(jsonString);
        var heroesTokens = responseTokens["data"]["constants"]["heroes"].Children().ToList();
        var heroesList = new List<HeroEntity>();
        foreach (var heroToken in heroesTokens)
        {
            var heroEntity = JsonConvert.DeserializeObject<HeroEntity>(heroToken.ToString(), new HeroConverter());
            heroesList.Add(heroEntity);
        }
        //var heroes = JsonConvert.DeserializeObject<List<HeroEntity>>(jsonString);

        Console.WriteLine("Done");
    }
}