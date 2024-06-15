using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DotaWordle.DataAcess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class RenameStartingAttributesToBaseAndClarifyHeroOwnership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_PrimaryAttributes_PrimaryAttributeId",
                table: "Heroes");

            migrationBuilder.DropTable(
                name: "PrimaryAttributes");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "RoleTypes");

            migrationBuilder.RenameColumn(
                name: "StartingMovespeed",
                table: "Heroes",
                newName: "MoveSpeedBase");

            migrationBuilder.RenameColumn(
                name: "StartingDamageMin",
                table: "Heroes",
                newName: "DamageMinBase");

            migrationBuilder.RenameColumn(
                name: "StartingDamageMax",
                table: "Heroes",
                newName: "DamageMaxBase");

            migrationBuilder.RenameColumn(
                name: "StartingArmor",
                table: "Heroes",
                newName: "ArmorBase");

            migrationBuilder.CreateTable(
                name: "HeroPrimaryAttributes",
                columns: table => new
                {
                    HeroPrimaryAttributeId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroPrimaryAttributes", x => x.HeroPrimaryAttributeId);
                });

            migrationBuilder.CreateTable(
                name: "HeroRoleTypes",
                columns: table => new
                {
                    HeroRoleTypeId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroRoleTypes", x => x.HeroRoleTypeId);
                });

            migrationBuilder.CreateTable(
                name: "HeroRoles",
                columns: table => new
                {
                    HeroId = table.Column<int>(type: "integer", nullable: false),
                    HeroRoleTypeId = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroRoles", x => new { x.HeroId, x.HeroRoleTypeId });
                    table.ForeignKey(
                        name: "FK_HeroRoles_HeroRoleTypes_HeroRoleTypeId",
                        column: x => x.HeroRoleTypeId,
                        principalTable: "HeroRoleTypes",
                        principalColumn: "HeroRoleTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroRoles_Heroes_HeroId",
                        column: x => x.HeroId,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "HeroPrimaryAttributes",
                columns: new[] { "HeroPrimaryAttributeId", "Name" },
                values: new object[,]
                {
                    { 0, "Strength" },
                    { 1, "Agility" },
                    { 2, "Intelligence" },
                    { 3, "All" }
                });

            migrationBuilder.InsertData(
                table: "HeroRoleTypes",
                columns: new[] { "HeroRoleTypeId", "Name" },
                values: new object[,]
                {
                    { 0, "Carry" },
                    { 1, "Disabler" },
                    { 2, "Durable" },
                    { 3, "Escape" },
                    { 4, "Initiator" },
                    { 5, "Jungler" },
                    { 6, "Pusher" },
                    { 7, "Nuker" },
                    { 8, "Support" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeroRoles_HeroRoleTypeId",
                table: "HeroRoles",
                column: "HeroRoleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_HeroPrimaryAttributes_PrimaryAttributeId",
                table: "Heroes",
                column: "PrimaryAttributeId",
                principalTable: "HeroPrimaryAttributes",
                principalColumn: "HeroPrimaryAttributeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_HeroPrimaryAttributes_PrimaryAttributeId",
                table: "Heroes");

            migrationBuilder.DropTable(
                name: "HeroPrimaryAttributes");

            migrationBuilder.DropTable(
                name: "HeroRoles");

            migrationBuilder.DropTable(
                name: "HeroRoleTypes");

            migrationBuilder.RenameColumn(
                name: "MoveSpeedBase",
                table: "Heroes",
                newName: "StartingMovespeed");

            migrationBuilder.RenameColumn(
                name: "DamageMinBase",
                table: "Heroes",
                newName: "StartingDamageMin");

            migrationBuilder.RenameColumn(
                name: "DamageMaxBase",
                table: "Heroes",
                newName: "StartingDamageMax");

            migrationBuilder.RenameColumn(
                name: "ArmorBase",
                table: "Heroes",
                newName: "StartingArmor");

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
                name: "Roles",
                columns: table => new
                {
                    HeroId = table.Column<int>(type: "integer", nullable: false),
                    RoleTypeId = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => new { x.HeroId, x.RoleTypeId });
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
                name: "IX_Roles_RoleTypeId",
                table: "Roles",
                column: "RoleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_PrimaryAttributes_PrimaryAttributeId",
                table: "Heroes",
                column: "PrimaryAttributeId",
                principalTable: "PrimaryAttributes",
                principalColumn: "PrimaryAttributeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
