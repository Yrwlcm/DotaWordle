using DataParser;
using DotaWordle.DataAcess.Postgres;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<HeroesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"),
        npgsqlOptionsAction => npgsqlOptionsAction.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

builder.Services.AddScoped<IHeroesStatisicsParser, StratzApiParser>();

builder.Services.AddHostedService<HeroesUpdateService>();
builder.Services.AddHostedService<WinratesUpdateService>();

var host = builder.Build();
host.Run();