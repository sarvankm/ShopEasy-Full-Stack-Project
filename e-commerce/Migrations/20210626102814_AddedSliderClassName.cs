using Microsoft.EntityFrameworkCore.Migrations;

namespace e_commerce.Migrations
{
    public partial class AddedSliderClassName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClassName",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassName",
                table: "Sliders");
        }
    }
}
