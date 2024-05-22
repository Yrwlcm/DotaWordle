using Moq;
using FluentAssertions;
using Dota_Wordle.Logic;
using DotaWordle.Models;

namespace DataParserTests.DotaWordleTests
{
    public class RandomHeroGeneratorTests
    {
        private Mock<IHeroRepository> heroRepositoryMock;
        private RandomHeroGenerator randomHeroGenerator;

        [SetUp]
        public void SetUp()
        {
            heroRepositoryMock = new Mock<IHeroRepository>();
            randomHeroGenerator = new RandomHeroGenerator(heroRepositoryMock.Object);
        }

        [Test]
        public void GetRandomHeroShouldReturnHeroIdFromRepository()
        {
            var heroes = new List<Hero>
            {
                new() { Id = 1 },
                new() { Id = 2 },
                new() { Id = 3 }
            };
            
            heroRepositoryMock.Setup(repo => repo.GetHeroes()).Returns(heroes);
            var result = randomHeroGenerator.GetRandomHero();
            
            result.Should().BeOneOf(heroes.Select(h => h.Id));
        }
    }
}