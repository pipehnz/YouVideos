using Microsoft.EntityFrameworkCore.Migrations;

namespace HelloWorldMVC.Migrations
{
    public partial class AgregarColumnaEsVisibleTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EsVisible",
                table: "Tags",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsVisible",
                table: "Tags");
        }
    }
}
