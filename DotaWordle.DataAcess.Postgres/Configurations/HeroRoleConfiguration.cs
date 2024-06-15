using DotaWordle.DataAcess.Postgres.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotaWordle.DataAcess.Postgres.Configurations;

public class HeroRoleConfiguration : IEntityTypeConfiguration<HeroRoleEntity>
{
    public void Configure(EntityTypeBuilder<HeroRoleEntity> builder)
    {
        builder.HasKey(role => new { role.HeroId, RoleTypeId = role.HeroRoleTypeId });

        builder.HasOne(role => role.Hero)
            .WithMany(hero => hero.Roles);

        builder.HasOne(role => role.RoleType)
            .WithMany(roleType => roleType.Roles)
            .HasForeignKey(role => role.HeroRoleTypeId);
    }
}