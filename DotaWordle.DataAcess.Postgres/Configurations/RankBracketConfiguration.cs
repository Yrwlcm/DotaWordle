using DotaWordle.DataAcess.Postgres.Enums;
using DotaWordle.DataAcess.Postgres.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotaWordle.DataAcess.Postgres.Configurations;

public class RankBracketConfiguration : IEntityTypeConfiguration<RankBracketEntity>
{
    public void Configure(EntityTypeBuilder<RankBracketEntity> builder)
    {
        builder.HasKey(bracket => bracket.RankBracketId);

        builder.HasMany(bracket => bracket.WeekWinrates)
            .WithOne(weekWinrate => weekWinrate.RankBracket)
            .HasForeignKey(weekWinrate => weekWinrate.RankBracketId);

        builder.HasData
        (
            Enum.GetValues(typeof(RankBracket))
                .Cast<RankBracket>()
                .Select(e => new RankBracketEntity
                {
                    RankBracketId = e,
                    Name = e.ToString()
                })
        );
    }
}