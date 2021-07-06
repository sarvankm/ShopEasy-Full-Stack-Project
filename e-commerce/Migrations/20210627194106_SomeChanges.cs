using Microsoft.EntityFrameworkCore.Migrations;

namespace e_commerce.Migrations
{
    public partial class SomeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Specs_PhoneSpecsId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "PhoneSpecsId",
                table: "Products",
                newName: "SpecsId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_PhoneSpecsId",
                table: "Products",
                newName: "IX_Products_SpecsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Specs_SpecsId",
                table: "Products",
                column: "SpecsId",
                principalTable: "Specs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Specs_SpecsId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "SpecsId",
                table: "Products",
                newName: "PhoneSpecsId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SpecsId",
                table: "Products",
                newName: "IX_Products_PhoneSpecsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Specs_PhoneSpecsId",
                table: "Products",
                column: "PhoneSpecsId",
                principalTable: "Specs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
