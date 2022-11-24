using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WantApp.Migrations
{
    public partial class ColunaPrecoProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Preco",
                table: "Produtos",
                type: "numeric(10,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Preco",
                table: "Produtos");
        }
    }
}
