using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RouletteDemo.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpinId",
                table: "Spins",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BetId",
                table: "Bets",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Spins",
                newName: "SpinId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Bets",
                newName: "BetId");
        }
    }
}
