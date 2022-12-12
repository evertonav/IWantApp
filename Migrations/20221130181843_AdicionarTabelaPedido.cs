using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WantApp.Migrations
{
    public partial class AdicionarTabelaPedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClienteId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    EnderecoEntrega = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CriadoPor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EditadoPor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DataEdicao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PedidosProdutos",
                columns: table => new
                {
                    PedidosId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutosId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosProdutos", x => new { x.PedidosId, x.ProdutosId });
                    table.ForeignKey(
                        name: "FK_PedidosProdutos_Pedido_PedidosId",
                        column: x => x.PedidosId,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidosProdutos_Produtos_ProdutosId",
                        column: x => x.ProdutosId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidosProdutos_ProdutosId",
                table: "PedidosProdutos",
                column: "ProdutosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidosProdutos");

            migrationBuilder.DropTable(
                name: "Pedido");
        }
    }
}
