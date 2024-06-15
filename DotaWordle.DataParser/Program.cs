using DataParser;
using DotaWordle.DataAcess.Postgres;
using DotaWordle.DataAcess.Postgres.Models;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddAutoMapper(
    cfg => cfg.CreateMap<HeroEntity, HeroEntity>()
        .ForMember(dest => dest.Id, opt => opt.Ignore())
        .ForMember(dest => dest.Roles, opt => opt.Ignore())
        .ForAllMembers(opts => opts.Condition((_, _, srcMember) => srcMember != null)));

builder.Services.AddDbContext<HeroesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"),
        npgsqlOptionsAction => npgsqlOptionsAction.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

builder.Services.AddScoped<IHeroesStatisicsParser, StratzApiParser>();

builder.Services.AddHostedService<HeroesUpdateService>();
builder.Services.AddHostedService<WinratesUpdateService>();

var host = builder.Build();
host.Run();