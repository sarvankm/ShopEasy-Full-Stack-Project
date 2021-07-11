using Microsoft.EntityFrameworkCore.Migrations;

namespace e_commerce.Migrations
{
    public partial class AddedBrendTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrendId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Brends",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryChildId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brends_CategoryChilds_CategoryChildId",
                        column: x => x.CategoryChildId,
                        principalTable: "CategoryChilds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrendId",
                table: "Products",
                column: "BrendId");

            migrationBuilder.CreateIndex(
                name: "IX_Brends_CategoryChildId",
                table: "Brends",
                column: "CategoryChildId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brends_BrendId",
                table: "Products",
                column: "BrendId",
                principalTable: "Brends",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brends_BrendId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Brends");

            migrationBuilder.DropIndex(
                name: "IX_Products_BrendId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BrendId",
                table: "Products");
        }
    }
}
