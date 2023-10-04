using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawnEcommerce.Migrations
{
    public partial class AddSaleProductId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleProducts",
                table: "SaleProducts");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SaleProducts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleProducts",
                table: "SaleProducts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SaleProducts_SaleId",
                table: "SaleProducts",
                column: "SaleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleProducts",
                table: "SaleProducts");

            migrationBuilder.DropIndex(
                name: "IX_SaleProducts_SaleId",
                table: "SaleProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SaleProducts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleProducts",
                table: "SaleProducts",
                columns: new[] { "SaleId", "ProductId" });
        }
    }
}
