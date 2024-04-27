using DataParser.Enums;
using DataParser.Models;
using Microsoft.EntityFrameworkCore;

namespace DataParser;

public sealed class ApplicationContext : DbContext
{
    public DbSet<HeroEntity> Heroes { get; set; } = null!;
    public DbSet<PrimaryAttributeEntity> PrimaryAttributes { get; set; } = null!;
    public DbSet<RoleTypeEntity> RoleTypes { get; set; } = null!;

    public ApplicationContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql($"Host=localhost;Port=5432;Database=DotaWordleDb;Username=postgres;" +
                                 $"Password={Environment.GetEnvironmentVariable("PostgreSqlPassword")}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<HeroEntity>()
            .HasMany(hero => hero.Roles)
            .WithOne(role => role.Hero)
            .HasForeignKey(role => role.HeroId);

        modelBuilder
            .Entity<HeroEntity>()
            .HasOne(hero => hero.PrimaryAttribute)
            .WithMany(attribute => attribute.Heroes)
            .HasForeignKey(role => role.PrimaryAttributeId);

        modelBuilder
            .Entity<RoleEntity>()
            .HasOne(role => role.RoleType)
            .WithMany(roleType => roleType.Roles)
            .HasForeignKey(role => role.RoleTypeId);

        modelBuilder
            .Entity<PrimaryAttributeEntity>()
            .HasData(Enum.GetValues(typeof(PrimaryAttribute))
                .Cast<PrimaryAttribute>()
                .Select(e => new PrimaryAttributeEntity
                {
                    PrimaryAttributeId = e,
                    Name = e.ToString()
                }));

        modelBuilder
            .Entity<RoleTypeEntity>()
            .Property(e => e.RoleTypeId)
            .HasConversion<int>();

        modelBuilder
            .Entity<RoleTypeEntity>()
            .HasData(Enum.GetValues(typeof(RoleType))
                .Cast<RoleType>()
                .Select(e => new RoleTypeEntity()
                {
                    RoleTypeId = e,
                    Name = e.ToString()
                }));
    }
}