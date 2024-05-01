using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DotaWordle.DataAcess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrimaryAttributes",
                columns: table => new
                {
                    PrimaryAttributeId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryAttributes", x => x.PrimaryAttributeId);
                });

            migrationBuilder.CreateTable(
                name: "RoleTypes",
                columns: table => new
                {
                    RoleTypeId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTypes", x => x.RoleTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Heroes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    GameVersion = table.Column<int>(type: "integer", nullable: false),
                    AttackType = table.Column<string>(type: "text", nullable: false),
                    StartingArmor = table.Column<float>(type: "real", nullable: false),
                    StartingDamageMin = table.Column<float>(type: "real", nullable: false),
                    StartingDamageMax = table.Column<float>(type: "real", nullable: false),
                    StartingMovespeed = table.Column<float>(type: "real", nullable: false),
                    AttackRange = table.Column<float>(type: "real", nullable: false),
                    PrimaryAttributeId = table.Column<int>(type: "integer", nullable: false),
                    StrengthBase = table.Column<float>(type: "real", nullable: false),
                    AgilityBase = table.Column<float>(type: "real", nullable: false),
                    IntelligenceBase = table.Column<float>(type: "real", nullable: false),
                    Complexity = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heroes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Heroes_PrimaryAttributes_PrimaryAttributeId",
                        column: x => x.PrimaryAttributeId,
                        principalTable: "PrimaryAttributes",
                        principalColumn: "PrimaryAttributeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HeroId = table.Column<int>(type: "integer", nullable: false),
                    RoleTypeId = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Heroes_HeroId",
                        column: x => x.HeroId,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Roles_RoleTypes_RoleTypeId",
                        column: x => x.RoleTypeId,
                        principalTable: "RoleTypes",
                        principalColumn: "RoleTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PrimaryAttributes",
                columns: new[] { "PrimaryAttributeId", "Name" },
                values: new object[,]
                {
                    { 0, "Strength" },
                    { 1, "Agility" },
                    { 2, "Intelligence" },
                    { 3, "All" }
                });

            migrationBuilder.InsertData(
                table: "RoleTypes",
                columns: new[] { "RoleTypeId", "Name" },
                values: new object[,]
                {
                    { 0, "Carry" },
                    { 1, "Escape" },
                    { 2, "Nuker" },
                    { 3, "Initiator" },
                    { 4, "Durable" },
                    { 5, "Disabler" },
                    { 6, "Jungler" },
                    { 7, "Support" },
                    { 8, "Pusher" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_PrimaryAttributeId",
                table: "Heroes",
                column: "PrimaryAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_HeroId",
                table: "Roles",
                column: "HeroId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleTypeId",
                table: "Roles",
                column: "RoleTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Heroes");

            migrationBuilder.DropTable(
                name: "RoleTypes");

            migrationBuilder.DropTable(
                name: "PrimaryAttributes");
        }
    }
}
