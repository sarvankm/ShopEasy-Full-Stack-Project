using Microsoft.EntityFrameworkCore.Migrations;

namespace e_commerce.Migrations
{
    public partial class ChangedSpecs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_PhoneSpecs_PhoneSpecsId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "PhoneSpecs");

            migrationBuilder.CreateTable(
                name: "Specs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProducerForView = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProducerValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductionYearForView = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductionYearValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeForView = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OSForView = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OSValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specs", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Specs_PhoneSpecsId",
                table: "Products",
                column: "PhoneSpecsId",
                principalTable: "Specs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Specs_PhoneSpecsId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Specs");

            migrationBuilder.CreateTable(
                name: "PhoneSpecs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneSpecs", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_PhoneSpecs_PhoneSpecsId",
                table: "Products",
                column: "PhoneSpecsId",
                principalTable: "PhoneSpecs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
