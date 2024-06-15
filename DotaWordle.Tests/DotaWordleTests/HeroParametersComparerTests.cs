using Dota_Wordle.Logic;
using DotaWordle.Models;
using FluentAssertions;

namespace DotaWordleTests.HeroParameters;

public class HeroParametersComparerTests
{
    private HeroParametersComparer parametersComparer;
    private Hero firstHero;
    private Hero secondHero;

    [SetUp]
    public void Setup()
    {
        parametersComparer = new HeroParametersComparer();

        firstHero = new Hero
        {
            Id = 1,
            Name = "Anti-Mage",
            AttackType = "Melee",
            ArmorBase = 5,
            DamageMinBase = 53,
            DamageMaxBase = 57,
            MoveSpeedBase = 310,
            AttackRange = 150,
            StrengthBase = 19,
            AgilityBase = 24,
            IntelligenceBase = 12,
            Complexity = 1,
            PrimaryAttributeName = "Agility",
            Roles = new List<Role>
            {
                new() { Name = "Carry", Level = 3 },
                new() { Name = "Escape", Level = 3 },
                new() { Name = "Nuker", Level = 1 },
            },
            WeekWinrates = new List<HeroWinrate>
            {
                new() { RankBracket = "Immortal", Winrate = 52 },
                new() { RankBracket = "Legend", Winrate = 53 },
                new() { RankBracket = "Herald", Winrate = 50 },
            }
        };

        secondHero = new Hero
        {
            Id = 3,
            Name = "Bane",
            AttackType = "Ranged",
            ArmorBase = 4.8333335f,
            DamageMinBase = -1,
            DamageMaxBase = 5,
            MoveSpeedBase = 305,
            AttackRange = 400,
            StrengthBase = 23,
            AgilityBase = 23,
            IntelligenceBase = 23,
            Complexity = 2,
            PrimaryAttributeName = "All",
            Roles = new List<Role>
            {
                new() { Name = "Nuker", Level = 1 },
                new() { Name = "Durable", Level = 1 },
                new() { Name = "Disabler", Level = 3 },
                new() { Name = "Support", Level = 2 },
            },
            WeekWinrates = new List<HeroWinrate>
            {
                new() { RankBracket = "Herald", Winrate = 47 },
                new() { RankBracket = "Legend", Winrate = 51 },
                new() { RankBracket = "Immortal", Winrate = 53 }
            }
        };
    }

    [Test]
    public void ShouldCorrectlyCompareHeroes()
    {
        var result = parametersComparer.CompareHeroes(firstHero, secondHero);
        
        var expectedResult = new HeroComparison
        {
            HeroName = firstHero.Name,
            ComparedHeroName = secondHero.Name,
            SameAttackType = false,
            ArmorBaseComparision = 1,
            DamageMinBaseComparision = 1,
            DamageMaxBaseComparision = 1,
            MoveSpeedBaseComparision = 1,
            AttackRangeComparision = -1,
            StrengthBaseComparision = -1,
            AgilityBaseComparision = 1,
            IntelligenceBaseComparision = -1,
            ComplexityComparision = -1,
            SamePrimaryAttributeName = false,
            RolesComparision = new List<RoleComparision>
            {
                new() { Name = "Carry", LevelComparision = 1 },
                new() { Name = "Escape", LevelComparision = 1 },
                new() { Name = "Nuker", LevelComparision = 0 },
                new() { Name = "Initiator", LevelComparision = 0 },
                new() { Name = "Durable", LevelComparision = -1 },
                new() { Name = "Disabler", LevelComparision = -1 },
                new() { Name = "Jungler", LevelComparision = 0 },
                new() { Name = "Support", LevelComparision = -1 },
                new() { Name = "Pusher", LevelComparision = 0 },
            },
            WeekWinratesComparision = new List<HeroWinrateComparision>
            {
                new() { RankBracket = "Herald", WinrateComparision = 1 },
                new() { RankBracket = "Legend", WinrateComparision = 1 },
                new() { RankBracket = "Immortal", WinrateComparision = -1 },
            }
        };
    
        result.Should().BeEquivalentTo(expectedResult);
    }
}