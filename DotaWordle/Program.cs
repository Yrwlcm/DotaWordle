using Dota_Wordle.Logic;
using DotaWordle.MappingProfiles;
using DotaWordle.DataAcess.Postgres;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddDbContext<HeroesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"),
        npgsqlOptionsAction => npgsqlOptionsAction.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(HeroProfile), typeof(RoleProfile), typeof(WinrateProfile));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HeroesDbContext>();

builder.Services.AddScoped<IHeroParametersComparer, HeroParametersComparer>();
builder.Services.AddScoped<IHeroRepository, HeroRepository>();
builder.Services.AddScoped<IRandomHeroGenerator, RandomHeroGenerator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.UseSession();

app.MapControllers();

app.MapRazorPages();

app.Run();