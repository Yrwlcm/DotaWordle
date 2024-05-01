using DotaWordle.DataAcess.Postgres.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotaWordle.DataAcess.Postgres.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasKey(role => new { role.HeroId, role.RoleTypeId });

        builder.HasOne(role => role.Hero)
            .WithMany(hero => hero.Roles);

        builder.HasOne(role => role.RoleType)
            .WithMany(roleType => roleType.Roles)
            .HasForeignKey(role => role.RoleTypeId);
    }
}