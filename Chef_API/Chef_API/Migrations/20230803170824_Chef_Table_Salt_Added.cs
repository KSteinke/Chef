using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chef_API.Migrations
{
    /// <inheritdoc />
    public partial class Chef_Table_Salt_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Chefs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Chefs");
        }
    }
}
