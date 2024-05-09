using AutoMapper;
using DotaWordle.DataAcess.Postgres.Models;
using DotaWordle.Models;

namespace DotaWordle.MappingProfiles;

public class WinrateProfile : Profile
{
    public WinrateProfile()
    {
        CreateMap<HeroWeekWinrateEntity, HeroWinrate>()
            .ForMember(dest => dest.RankBracket, opt => opt.MapFrom(src => src.RankBracketId.ToString()))
            .ForMember(dest => dest.Winrate,
                opt => opt.MapFrom(src => Math.Round((float)src.Wins * 100 / src.Matches)));
    }
}