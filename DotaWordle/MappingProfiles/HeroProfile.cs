using AutoMapper;
using DotaWordle.DataAcess.Postgres.Models;
using DotaWordle.Models;

namespace DotaWordle.MappingProfiles;

public class HeroProfile : Profile
{
    public HeroProfile()
    {
        CreateMap<HeroEntity, Hero>()
            .ForMember(dest => dest.PrimaryAttributeName, opt => opt.MapFrom(src => src.PrimaryAttributeId.ToString()));
    }
}