using DotaWordle.DataAcess.Postgres.Enums;
using DotaWordle.DataAcess.Postgres.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotaWordle.DataAcess.Postgres.Configurations;

public class PrimaryAttributeConfiguration : IEntityTypeConfiguration<PrimaryAttributeEntity>
{
    public void Configure(EntityTypeBuilder<PrimaryAttributeEntity> builder)
    {
        builder.HasKey(attribute => attribute.PrimaryAttributeId);

        builder.HasMany(attribute => attribute.Heroes)
            .WithOne(hero => hero.PrimaryAttribute)
            .HasForeignKey(attribute => attribute.PrimaryAttributeId);

        builder.HasData
        (
            Enum.GetValues(typeof(PrimaryAttribute))
                .Cast<PrimaryAttribute>()
                .Select(e => new PrimaryAttributeEntity
                {
                    PrimaryAttributeId = e,
                    Name = e.ToString()
                })
        );
    }
}