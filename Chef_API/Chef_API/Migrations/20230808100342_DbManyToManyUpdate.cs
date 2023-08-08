using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chef_API.Migrations
{
    /// <inheritdoc />
    public partial class DbManyToManyUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeIngredient",
                table: "RecipeIngredient");

            migrationBuilder.AddColumn<int>(
                name: "RecipeIngredientId",
                table: "RecipeIngredient",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeIngredient",
                table: "RecipeIngredient",
                column: "RecipeIngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_IngredientId",
                table: "RecipeIngredient",
                column: "IngredientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeIngredient",
                table: "RecipeIngredient");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredient_IngredientId",
                table: "RecipeIngredient");

            migrationBuilder.DropColumn(
                name: "RecipeIngredientId",
                table: "RecipeIngredient");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeIngredient",
                table: "RecipeIngredient",
                columns: new[] { "IngredientId", "RecipeId" });
        }
    }
}
