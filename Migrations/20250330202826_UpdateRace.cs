using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eldrin.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 12u);

            migrationBuilder.InsertData(
                table: "Races",
                columns: new[] { "Id", "AgilityBonus", "HealthBonus", "MagicBonus", "Name", "StartingCity", "StrengthBonus" },
                values: new object[] { 4u, 3, 15, 0, "Лизардмен", "Болотные Хижины", 5 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 4u);

            migrationBuilder.InsertData(
                table: "Races",
                columns: new[] { "Id", "AgilityBonus", "HealthBonus", "MagicBonus", "Name", "StartingCity", "StrengthBonus" },
                values: new object[] { 12u, 3, 15, 0, "Лизардмен", "Болотные Хижины", 5 });
        }
    }
}
