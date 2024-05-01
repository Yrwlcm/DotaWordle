using DotaWordle.DataAcess.Postgres.Enums;
using DotaWordle.DataAcess.Postgres.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotaWordle.DataAcess.Postgres.Configurations;

public class RoleTypeConfiguration : IEntityTypeConfiguration<RoleTypeEntity>
{
    public void Configure(EntityTypeBuilder<RoleTypeEntity> builder)
    {
        builder.HasKey(roleType => roleType.RoleTypeId);

        builder.HasMany(roleType => roleType.Roles)
            .WithOne(role => role.RoleType)
            .HasForeignKey(role => role.RoleTypeId);

        builder.HasData
        (
            Enum.GetValues(typeof(RoleType))
                .Cast<RoleType>()
                .Select(e => new RoleTypeEntity()
                {
                    RoleTypeId = e,
                    Name = e.ToString()
                })
        );
    }
}