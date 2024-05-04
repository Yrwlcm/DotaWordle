using DotaWordle.DataAcess.Postgres.Enums;
namespace DataParser;

public static class Program
{
    static async Task Main(string[] args)
    {
        var client = new HttpClient();

        var heroesList = await StratzApi.ParseAllHeroes(client);
        var winrates = await StratzApi.ParseAllHeroesWeekWinrates(client,
            [RankBracket.Herald, RankBracket.Legend, RankBracket.Immortal]);
        
        var flatWinrates = winrates.SelectMany(x => x).ToList();

        using (var db = new ApplicationContext())
        {
            //TODO: separate heroes and winrates parsing
            db.Heroes.UpdateRange(heroesList);
            db.HeroWeekWinrates.UpdateRange(flatWinrates);
            await db.SaveChangesAsync();
        }

        Console.WriteLine("Done");
    }
}