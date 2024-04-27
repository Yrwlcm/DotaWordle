using DataParser.Serializers;
using DotaWordle.DataAcess.Postgres.Enums;
using DotaWordle.DataAcess.Postgres.Models;
using FluentAssertions;
using Newtonsoft.Json;

namespace DataParserTests;

public class HeroConverterTests
{
    private string json;

    [SetUp]
    public void Setup()
    {
        json = """
               {
                 "id": 1,
                 "displayName": "Anti-Mage",
                 "gameVersionId": 172,
                 "roles": [
                   {
                     "roleId": "CARRY",
                     "level": 3
                   },
                   {
                     "roleId": "ESCAPE",
                     "level": 3
                   },
                   {
                     "roleId": "NUKER",
                     "level": 1
                   }
                 ],
                 "stats": {
                   "attackType": "Melee",
                   "startingArmor": 5,
                   "startingDamageMin": 53,
                   "startingDamageMax": 57,
                   "attackRange": 150,
                   "primaryAttribute": "agi",
                   "strengthBase": 19,
                   "agilityBase": 24,
                   "intelligenceBase": 12,
                   "moveSpeed": 310,
                   "complexity": 1
                 }
               }
               """;
    }

    [Test]
    public void ShouldDeserializeCorrectJson()
    {
        var expectedHero = new HeroEntity
        {
            Id = 1,
            Name = "Anti-Mage",
            GameVersion = 172,
            Roles = new List<RoleEntity>()
            {
                new() { HeroId = 1, RoleTypeId = RoleType.Carry, Level = 3 },
                new() { HeroId = 1, RoleTypeId = RoleType.Escape, Level = 3 },
                new() { HeroId = 1, RoleTypeId = RoleType.Nuker, Level = 1 },
            },
            AttackType = "Melee",
            StartingArmor = 5,
            StartingDamageMin = 53,
            StartingDamageMax = 57,
            StartingMovespeed = 310,
            AttackRange = 150,
            PrimaryAttributeId = PrimaryAttribute.Agility,
            StrengthBase = 19,
            AgilityBase = 24,
            IntelligenceBase = 12,
            Complexity = 1,
        };
        
        var deserializedHero = JsonConvert.DeserializeObject<HeroEntity>(json, new HeroConverter());
        deserializedHero.Should().BeEquivalentTo(expectedHero, o => o.Excluding(p => p.Id));
    }
}