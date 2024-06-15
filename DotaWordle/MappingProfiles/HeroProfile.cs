using AutoMapper;
using DotaWordle.DataAcess.Postgres.Enums;
using DotaWordle.DataAcess.Postgres.Models;
using DotaWordle.Models;

namespace DotaWordle.MappingProfiles;

public class HeroProfile : Profile
{
    public HeroProfile()
    {
        CreateMap<HeroEntity, Hero>()
            .ForMember(dest => dest.PrimaryAttributeName,
                opt => opt.MapFrom(src => MapAttribute(src.PrimaryAttributeId)))
            .AfterMap(FillRoles);
    }

    private static string MapAttribute(HeroPrimaryAttribute attributeId)
    {
        return attributeId == HeroPrimaryAttribute.All ? "Universal" : attributeId.ToString();
    }

    private static void FillRoles(HeroEntity entity, Hero hero)
    {
        var existingRoles = hero.Roles.Select(role => role.Name).ToHashSet();
        var additionalRoles = Enum
            .GetNames(typeof(HeroRoleType))
            .Where(role => !existingRoles.Contains(role))
            .Select(role => new Role() { Name = role, Level = 0 })
            .ToList();
        hero.Roles = hero.Roles.Concat(additionalRoles).OrderBy(role => role.Name).ToList();
    }
}