using Microsoft.EntityFrameworkCore.Migrations;

namespace e_commerce.Migrations
{
    public partial class SomeColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryChilds_Categories_CategoryId",
                table: "CategoryChilds");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryChilds_Categories_CategoryId",
                table: "CategoryChilds",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryChilds_Categories_CategoryId",
                table: "CategoryChilds");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryChilds_Categories_CategoryId",
                table: "CategoryChilds",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
