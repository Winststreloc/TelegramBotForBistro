using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WriteOffUley.Migrations
{
    public partial class createSemifinishedProductTable3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "SemiFinishedProducts");

            migrationBuilder.AlterColumn<bool>(
                name: "LiquidOrSolid",
                table: "SemiFinishedProducts",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "LiquidOrSolid",
                table: "SemiFinishedProducts",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "SemiFinishedProducts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
