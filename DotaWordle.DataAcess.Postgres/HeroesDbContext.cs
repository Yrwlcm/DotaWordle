using DotaWordle.DataAcess.Postgres.Configurations;
using DotaWordle.DataAcess.Postgres.Models;
using Microsoft.EntityFrameworkCore;

namespace DotaWordle.DataAcess.Postgres;

public sealed class HeroesDbContext(DbContextOptions<HeroesDbContext> options) : DbContext(options)
{
    public DbSet<HeroEntity> Heroes { get; set; } = null!;
    public DbSet<HeroRoleEntity> HeroRoles { get; set; } = null!;
    public DbSet<HeroPrimaryAttributeEntity> HeroPrimaryAttributes { get; set; } = null!;
    public DbSet<HeroRoleTypeEntity> HeroRoleTypes { get; set; } = null!;
    public DbSet<RankBracketEntity> RankBrackets { get; set; } = null!;
    public DbSet<HeroWeekWinrateEntity> HeroWeekWinrates { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.ApplyConfiguration(new HeroConfiguration());
        modelBuilder.ApplyConfiguration(new HeroRoleConfiguration());
        modelBuilder.ApplyConfiguration(new HeroPrimaryAttributeConfiguration());
        modelBuilder.ApplyConfiguration(new HeroRoleTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RankBracketConfiguration());
        modelBuilder.ApplyConfiguration(new HeroWeekWinrateConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}