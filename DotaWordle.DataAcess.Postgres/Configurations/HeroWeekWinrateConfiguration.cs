using DotaWordle.DataAcess.Postgres.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotaWordle.DataAcess.Postgres.Configurations;

public class HeroWeekWinrateConfiguration : IEntityTypeConfiguration<HeroWeekWinrateEntity>
{
    public void Configure(EntityTypeBuilder<HeroWeekWinrateEntity> builder)
    {
        builder.HasKey(winrate => new {winrate.HeroId, winrate.RankBracketId});

        builder
            .HasOne(winrate => winrate.Hero)
            .WithMany(hero => hero.WeekWinrates);
        
        builder
            .HasOne(winrate => winrate.RankBracket)
            .WithMany(bracket => bracket.WeekWinrates)
            .HasForeignKey(winrate => winrate.RankBracketId);
    }
}