using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelationShip.Migrations
{
    /// <inheritdoc />
    public partial class AddRattingForPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Ratting",
                table: "Posts",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ratting",
                table: "Posts");
        }
    }
}
