using DotaWordle.DataAcess.Postgres.Configurations;
using DotaWordle.DataAcess.Postgres.Models;
using Microsoft.EntityFrameworkCore;

namespace DotaWordle.DataAcess.Postgres;

public sealed class HeroesDbContext(DbContextOptions<HeroesDbContext> options) : DbContext(options)
{
    public DbSet<HeroEntity> Heroes { get; set; } = null!;
    public DbSet<RoleEntity> Roles { get; set; } = null!;
    public DbSet<PrimaryAttributeEntity> PrimaryAttributes { get; set; } = null!;
    public DbSet<RoleTypeEntity> RoleTypes { get; set; } = null!;
    public DbSet<RankBracketEntity> RankBrackets { get; set; } = null!;
    public DbSet<HeroWeekWinrateEntity> HeroWeekWinrates { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.ApplyConfiguration(new HeroConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new PrimaryAttributeConfiguration());
        modelBuilder.ApplyConfiguration(new RoleTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RankBracketConfiguration());
        modelBuilder.ApplyConfiguration(new HeroWeekWinrateConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}