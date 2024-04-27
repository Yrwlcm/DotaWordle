﻿using DotaWordle.DataAcess.Postgres.Configurations;
using DotaWordle.DataAcess.Postgres.Models;
using Microsoft.EntityFrameworkCore;

namespace DataParser;

public sealed class ApplicationContext : DbContext
{
    public DbSet<HeroEntity> Heroes { get; set; } = null!;
    public DbSet<RoleEntity> Roles { get; set; } = null!;
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
        modelBuilder.ApplyConfiguration(new HeroConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new PrimaryAttributeConfiguration());
        modelBuilder.ApplyConfiguration(new RoleTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}