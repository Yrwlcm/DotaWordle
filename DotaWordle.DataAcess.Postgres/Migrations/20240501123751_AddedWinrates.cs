using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DotaWordle.DataAcess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddedWinrates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RankBrackets",
                columns: table => new
                {
                    RankBracketId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankBrackets", x => x.RankBracketId);
                });

            migrationBuilder.CreateTable(
                name: "HeroWeekWinrates",
                columns: table => new
                {
                    HeroId = table.Column<int>(type: "integer", nullable: false),
                    RankBracketId = table.Column<int>(type: "integer", nullable: false),
                    Wins = table.Column<long>(type: "bigint", nullable: false),
                    Matches = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroWeekWinrates", x => new { x.HeroId, x.RankBracketId });
                    table.ForeignKey(
                        name: "FK_HeroWeekWinrates_Heroes_HeroId",
                        column: x => x.HeroId,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroWeekWinrates_RankBrackets_RankBracketId",
                        column: x => x.RankBracketId,
                        principalTable: "RankBrackets",
                        principalColumn: "RankBracketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RankBrackets",
                columns: new[] { "RankBracketId", "Name" },
                values: new object[,]
                {
                    { 0, "Herald" },
                    { 1, "Guardian" },
                    { 2, "Crusader" },
                    { 3, "Archon" },
                    { 4, "Legend" },
                    { 5, "Ancient" },
                    { 6, "Divine" },
                    { 7, "Immortal" },
                    { 8, "Unknown" },
                    { 9, "All" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeroWeekWinrates_RankBracketId",
                table: "HeroWeekWinrates",
                column: "RankBracketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeroWeekWinrates");

            migrationBuilder.DropTable(
                name: "RankBrackets");
        }
    }
}
