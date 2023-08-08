using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chef_API.Migrations
{
    /// <inheritdoc />
    public partial class DbManyToManyUpdate_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecipeIngredientId",
                table: "RecipeIngredient",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RecipeIngredient",
                newName: "RecipeIngredientId");
        }
    }
}
