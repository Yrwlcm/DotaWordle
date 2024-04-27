using System.Net.Http.Json;

namespace DataParserTests;

[TestFixture]
public class StratzApiTests
{
    [Test]
    public Task GetCorrectResponse()
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
        var apiKey = Environment.GetEnvironmentVariable("StratzApiKey");
        request.Headers.Add("Authorization", $"Bearer {apiKey}");

        var responce = client.Send(request);
        return VerifyJson(responce.Content.ReadAsStringAsync());
    }
}