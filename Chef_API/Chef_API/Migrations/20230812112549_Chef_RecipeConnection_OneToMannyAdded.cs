using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chef_API.Migrations
{
    /// <inheritdoc />
    public partial class Chef_RecipeConnection_OneToMannyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Recipes_AuthorId",
                table: "Recipes",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Chefs_AuthorId",
                table: "Recipes",
                column: "AuthorId",
                principalTable: "Chefs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Chefs_AuthorId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_AuthorId",
                table: "Recipes");
        }
    }
}
