using DotaWordle.DataAcess.Postgres.Enums;
using DotaWordle.DataAcess.Postgres.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotaWordle.DataAcess.Postgres.Configurations;

public class HeroPrimaryAttributeConfiguration : IEntityTypeConfiguration<HeroPrimaryAttributeEntity>
{
    public void Configure(EntityTypeBuilder<HeroPrimaryAttributeEntity> builder)
    {
        builder.HasKey(attribute => attribute.HeroPrimaryAttributeId);

        builder.HasMany(attribute => attribute.Heroes)
            .WithOne(hero => hero.PrimaryAttribute)
            .HasForeignKey(attribute => attribute.PrimaryAttributeId);

        builder.HasData
        (
            Enum.GetValues(typeof(HeroPrimaryAttribute))
                .Cast<HeroPrimaryAttribute>()
                .Select(e => new HeroPrimaryAttributeEntity
                {
                    HeroPrimaryAttributeId = e,
                    Name = e.ToString()
                })
        );
    }
}