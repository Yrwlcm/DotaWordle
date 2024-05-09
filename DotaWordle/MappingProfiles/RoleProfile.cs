using AutoMapper;
using DotaWordle.DataAcess.Postgres.Models;
using DotaWordle.Models;

namespace DotaWordle.MappingProfiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<RoleEntity, Role>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RoleTypeId.ToString()));
    }
}