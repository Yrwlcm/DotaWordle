using AutoMapper;
using DotaWordle.DataAcess.Postgres.Enums;
using DotaWordle.MappingProfiles;
using DotaWordle.Models;
using DotaWordle.DataAcess.Postgres.Models;
using FluentAssertions;

namespace DotaWordleTests.MappingProfiles;

public class MappingProfilesTests
{
    private static Profile[] Profiles =
    [
        new HeroProfile(),
        new RoleProfile(),
        new WinrateProfile(),
    ];

    private static HeroEntity? heroEntityExample;
    private static List<RoleEntity>? roleEntitiesExamples;
    private static List<HeroWeekWinrateEntity>? winrateEntityExamples;

    private static MapperConfiguration mapperConfiguration;
    private static IMapper mapper;

    [SetUp]
    public static void Setup()
    {
        winrateEntityExamples = new List<HeroWeekWinrateEntity>()
        {
            new() { HeroId = 1, Matches = 100, Wins = 55, RankBracketId = RankBracket.Herald },
            new() { HeroId = 1, Matches = 100, Wins = 35, RankBracketId = RankBracket.Legend }
        };
        roleEntitiesExamples = new List<RoleEntity>()
        {
            new() { HeroId = 1, RoleTypeId = RoleType.Carry, Level = 3 },
            new() { HeroId = 1, RoleTypeId = RoleType.Escape, Level = 2 },
        };
        heroEntityExample = new HeroEntity()
        {
            Id = 1,
            Name = "Anti-mage",
            PrimaryAttributeId = PrimaryAttribute.Agility,
            AttackRange = 150,
            AgilityBase = 23,
        };

        mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfiles(Profiles));
        mapper = mapperConfiguration.CreateMapper();
    }

    [Test]
    public void AutoMapperConfigurationIsValid()
    {
        mapperConfiguration.AssertConfigurationIsValid();
    }

    [Test]
    public void RoleProfileShouldMapCorrectly()
    {
        var resultRoles = mapper.Map<List<Role>>(roleEntitiesExamples);

        var expectedRoles = new List<Role>
        {
            new() { Name = "Carry", Level = 3 },
            new() { Name = "Escape", Level = 2 },
        };

        resultRoles.Should().BeEquivalentTo(expectedRoles);
    }

    [Test]
    public void WinrateProfileShouldMapCorrectly()
    {
        var resultWinrates = mapper.Map<List<HeroWinrate>>(winrateEntityExamples);

        var expectedWinrates = new List<HeroWinrate>
        {
            new() { RankBracket = "Herald", Winrate = 55 },
            new() { RankBracket = "Legend", Winrate = 35 },
        };

        resultWinrates.Should().BeEquivalentTo(expectedWinrates);
    }

    [Test]
    public void HeroProfileShouldMapCorrectly_WithoutWinratesAndRoles()
    {
        var resultHero = mapper.Map<Hero>(heroEntityExample);

        var expectedHero = new Hero
        {
            Id = 1,
            Name = "Anti-mage",
            PrimaryAttributeName = "Agility",
            AttackRange = 150,
            AgilityBase = 23,
        };

        resultHero.Should()
            .BeEquivalentTo(expectedHero, o => o.Excluding(x => x.WeekWinrates).Excluding(x => x.Roles));
    }

    [Test]
    public void HeroProfileShouldMapCorrectly_WithWinratesAndRoles()
    {
        var fullHeroEntity = heroEntityExample with
        {
            WeekWinrates = winrateEntityExamples, Roles = roleEntitiesExamples
        };

        var resultHero = mapper.Map<Hero>(fullHeroEntity);

        var expectedHero = new Hero
        {
            Id = 1,
            Name = "Anti-mage",
            PrimaryAttributeName = "Agility",
            AttackRange = 150,
            AgilityBase = 23,
            Roles = new List<Role>
            {
                new() { Name = "Carry", Level = 3 }, new() { Name = "Escape", Level = 2 },
                new() { Name = "Disabler", Level = 0, }, new() { Name = "Durable", Level = 0, },
                new() { Name = "Initiator", Level = 0, }, new() { Name = "Jungler", Level = 0, },
                new() { Name = "Nuker", Level = 0, }, new() { Name = "Pusher", Level = 0, },
                new() { Name = "Support", Level = 0, },
            },
            WeekWinrates = new List<HeroWinrate>
                { new() { RankBracket = "Herald", Winrate = 55 }, new() { RankBracket = "Legend", Winrate = 35 } },
        };

        resultHero.Should().BeEquivalentTo(expectedHero);
    }
}