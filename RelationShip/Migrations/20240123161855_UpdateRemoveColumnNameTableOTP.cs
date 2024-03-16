using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelationShip.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRemoveColumnNameTableOTP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Otps");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Name",
                table: "Otps",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
