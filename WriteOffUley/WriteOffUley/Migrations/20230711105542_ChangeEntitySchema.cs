using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WriteOffUley.Migrations
{
    public partial class ChangeEntitySchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Operations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Operations");
        }
    }
}
