using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelationShip.Migrations
{
    /// <inheritdoc />
    public partial class AddGenderForCardUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Card",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Card");
        }
    }
}
