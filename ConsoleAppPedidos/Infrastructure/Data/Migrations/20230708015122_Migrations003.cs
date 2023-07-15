using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleAppPedidos.Migrations
{
    /// <inheritdoc />
    public partial class Migrations003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ItensDePedido_PedidoID",
                table: "ItensDePedido",
                column: "PedidoID");

            migrationBuilder.CreateIndex(
                name: "IX_ItensDePedido_ProdutoID",
                table: "ItensDePedido",
                column: "ProdutoID");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensDePedido_Pedidos_PedidoID",
                table: "ItensDePedido",
                column: "PedidoID",
                principalTable: "Pedidos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensDePedido_Produtos_ProdutoID",
                table: "ItensDePedido",
                column: "ProdutoID",
                principalTable: "Produtos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensDePedido_Pedidos_PedidoID",
                table: "ItensDePedido");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensDePedido_Produtos_ProdutoID",
                table: "ItensDePedido");

            migrationBuilder.DropIndex(
                name: "IX_ItensDePedido_PedidoID",
                table: "ItensDePedido");

            migrationBuilder.DropIndex(
                name: "IX_ItensDePedido_ProdutoID",
                table: "ItensDePedido");
        }
    }
}
