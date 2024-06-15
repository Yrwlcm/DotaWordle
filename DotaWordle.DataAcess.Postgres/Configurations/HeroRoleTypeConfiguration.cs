using DotaWordle.DataAcess.Postgres.Enums;
using DotaWordle.DataAcess.Postgres.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotaWordle.DataAcess.Postgres.Configurations;

public class HeroRoleTypeConfiguration : IEntityTypeConfiguration<HeroRoleTypeEntity>
{
    public void Configure(EntityTypeBuilder<HeroRoleTypeEntity> builder)
    {
        builder.HasKey(roleType => roleType.HeroRoleTypeId);

        builder.HasMany(roleType => roleType.Roles)
            .WithOne(role => role.RoleType)
            .HasForeignKey(role => role.HeroRoleTypeId);

        builder.HasData
        (
            Enum.GetValues(typeof(HeroRoleType))
                .Cast<HeroRoleType>()
                .Select(e => new HeroRoleTypeEntity()
                {
                    HeroRoleTypeId = e,
                    Name = e.ToString()
                })
        );
    }
}