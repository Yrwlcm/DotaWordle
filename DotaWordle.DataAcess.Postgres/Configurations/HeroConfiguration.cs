using DotaWordle.DataAcess.Postgres.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotaWordle.DataAcess.Postgres.Configurations;

public class HeroConfiguration : IEntityTypeConfiguration<HeroEntity>
{
    public void Configure(EntityTypeBuilder<HeroEntity> builder)
    {
        builder
            .HasKey(hero => hero.Id);

        builder
            .HasMany(hero => hero.Roles)
            .WithOne(role => role.Hero)
            .HasForeignKey(role => role.HeroId);;

        builder
            .HasOne(hero => hero.PrimaryAttribute)
            .WithMany(attribute => attribute.Heroes)
            .HasForeignKey(role => role.PrimaryAttributeId);

        builder
            .HasMany(hero => hero.WeekWinrates)
            .WithOne(winrate => winrate.Hero);
    }
}