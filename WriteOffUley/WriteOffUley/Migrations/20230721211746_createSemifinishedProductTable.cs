using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WriteOffUley.Migrations
{
    public partial class createSemifinishedProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSemiFinshedProducts_Products_ProudctId",
                table: "ProductSemiFinshedProducts");

            migrationBuilder.RenameColumn(
                name: "ProudctId",
                table: "ProductSemiFinshedProducts",
                newName: "ProductId");

            migrationBuilder.AddColumn<bool>(
                name: "LiquidOrSolid",
                table: "SemiFinishedProducts",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "ProductSemiFinshedProducts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSemiFinshedProducts_Products_ProductId",
                table: "ProductSemiFinshedProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSemiFinshedProducts_Products_ProductId",
                table: "ProductSemiFinshedProducts");

            migrationBuilder.DropColumn(
                name: "LiquidOrSolid",
                table: "SemiFinishedProducts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductSemiFinshedProducts");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductSemiFinshedProducts",
                newName: "ProudctId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSemiFinshedProducts_Products_ProudctId",
                table: "ProductSemiFinshedProducts",
                column: "ProudctId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
