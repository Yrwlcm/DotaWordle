using System.Collections.Immutable;
using DotaWordle.DataAcess.Postgres.Enums;
using DotaWordle.Models;

namespace Dota_Wordle.Logic;

public class HeroParametersComparer : IHeroParametersComparer
{
    private readonly ImmutableArray<RankBracket> comparingRankBrackets =
    [
        RankBracket.Herald,
        RankBracket.Legend,
        RankBracket.Immortal
    ];

    public HeroParametersComparer()
    {
        //TODO: Создать класс-конфигуратор
    }

    public HeroComparison CompareHeroes(Hero hero, Hero comparedHero)
    {
        return new HeroComparison
        {
            HeroName = hero.Name,
            ComparedHeroName = comparedHero.Name,
            SameAttackType =
                string.Equals(hero.AttackType, comparedHero.AttackType, StringComparison.OrdinalIgnoreCase),
            StartingArmorComparision = hero.StartingArmor.CompareTo(comparedHero.StartingArmor),
            StartingDamageMinComparision = hero.StartingDamageMin.CompareTo(comparedHero.StartingDamageMin),
            StartingDamageMaxComparision = hero.StartingDamageMax.CompareTo(comparedHero.StartingDamageMax),
            StartingMovespeedComparision = hero.StartingMovespeed.CompareTo(comparedHero.StartingMovespeed),
            AttackRangeComparision = hero.AttackRange.CompareTo(comparedHero.AttackRange),
            StrengthBaseComparision = hero.StrengthBase.CompareTo(comparedHero.StrengthBase),
            AgilityBaseComparision = hero.AgilityBase.CompareTo(comparedHero.AgilityBase),
            IntelligenceBaseComparision = hero.IntelligenceBase.CompareTo(comparedHero.IntelligenceBase),
            ComplexityComparision = hero.Complexity.CompareTo(comparedHero.Complexity),
            SamePrimaryAttributeName = string.Equals(hero.PrimaryAttributeName, comparedHero.PrimaryAttributeName,
                StringComparison.OrdinalIgnoreCase),
            RolesComparision = CompareAllRoles(hero.Roles, comparedHero.Roles),
            WeekWinratesComparision = CompareWinrates(hero.WeekWinrates, comparedHero.WeekWinrates),
        };
    }

    // Попытка зарефакторить эту функцию и вынести общую логику с CompareWinrates
    // Ведет к ужасному методу с кучей дженериков и селекторов
    // Что как по мне снижает читаемость и увеличивает сложность кода
    //TODO: Попытаться зарефакторить :)
    private List<RoleComparision> CompareAllRoles(List<Role> roles, List<Role> comparedRoles)
    {
        var roleComparisons = new List<RoleComparision>();

        foreach (var roleType in Enum.GetValues(typeof(RoleType)))
        {
            var roleName = roleType.ToString();
            var firstHeroRole = roles.FirstOrDefault(x => x.Name == roleName);
            var secondHeroRole = comparedRoles.FirstOrDefault(x => x.Name == roleName);
            roleComparisons.Add(CompareRole(roleName, firstHeroRole, secondHeroRole));
        }

        return roleComparisons;
    }

    private List<HeroWinrateComparision> CompareWinrates(List<HeroWinrate> heroWinrates,
        List<HeroWinrate> comparedHeroWinrates)
    {
        var winrateComparisons = new List<HeroWinrateComparision>();

        var rankBrackets = Enum.GetValues(typeof(RankBracket))
            .Cast<RankBracket>()
            .Where(x => comparingRankBrackets.Contains(x));

        foreach (var rankBracket in rankBrackets)
        {
            var rankName = rankBracket.ToString();
            var firstHeroWinrate = heroWinrates.FirstOrDefault(x => x.RankBracket == rankName);
            var secondHeroWinrate = comparedHeroWinrates.FirstOrDefault(x => x.RankBracket == rankName);
            winrateComparisons.Add(CompareWinrate(rankName, firstHeroWinrate, secondHeroWinrate));
        }

        return winrateComparisons;
    }

    private HeroWinrateComparision CompareWinrate(string rankName, HeroWinrate? first, HeroWinrate? second)
    {
        return new HeroWinrateComparision
        {
            RankBracket = rankName,
            WinrateComparision = first?.CompareTo(second) ?? (second == null ? 0 : -1)
        };
    }


    private RoleComparision CompareRole(string roleName, Role? first, Role? second)
    {
        return new RoleComparision
        {
            Name = roleName,
            LevelComparision = first?.CompareTo(second) ?? (second == null ? 0 : -1)
        };
    }
}