using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WriteOffUley.Migrations
{
    public partial class createSemifinishedProductTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSemiFinshedProducts_Products_ProductId",
                table: "ProductSemiFinshedProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSemiFinshedProducts_SemiFinishedProducts_SemiFinishedProductId",
                table: "ProductSemiFinshedProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSemiFinshedProducts",
                table: "ProductSemiFinshedProducts");

            migrationBuilder.RenameTable(
                name: "ProductSemiFinshedProducts",
                newName: "ProductSemiFinishedProducts");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSemiFinshedProducts_SemiFinishedProductId",
                table: "ProductSemiFinishedProducts",
                newName: "IX_ProductSemiFinishedProducts_SemiFinishedProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSemiFinishedProducts",
                table: "ProductSemiFinishedProducts",
                columns: new[] { "ProductId", "SemiFinishedProductId" });

            migrationBuilder.CreateTable(
                name: "Storage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storage", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSemiFinishedProducts_Products_ProductId",
                table: "ProductSemiFinishedProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSemiFinishedProducts_SemiFinishedProducts_SemiFinishedProductId",
                table: "ProductSemiFinishedProducts",
                column: "SemiFinishedProductId",
                principalTable: "SemiFinishedProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSemiFinishedProducts_Products_ProductId",
                table: "ProductSemiFinishedProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSemiFinishedProducts_SemiFinishedProducts_SemiFinishedProductId",
                table: "ProductSemiFinishedProducts");

            migrationBuilder.DropTable(
                name: "Storage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSemiFinishedProducts",
                table: "ProductSemiFinishedProducts");

            migrationBuilder.RenameTable(
                name: "ProductSemiFinishedProducts",
                newName: "ProductSemiFinshedProducts");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSemiFinishedProducts_SemiFinishedProductId",
                table: "ProductSemiFinshedProducts",
                newName: "IX_ProductSemiFinshedProducts_SemiFinishedProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSemiFinshedProducts",
                table: "ProductSemiFinshedProducts",
                columns: new[] { "ProductId", "SemiFinishedProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSemiFinshedProducts_Products_ProductId",
                table: "ProductSemiFinshedProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSemiFinshedProducts_SemiFinishedProducts_SemiFinishedProductId",
                table: "ProductSemiFinshedProducts",
                column: "SemiFinishedProductId",
                principalTable: "SemiFinishedProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
