using ConsoleAppPedidos.Models;

namespace ConsoleAppPedidos.Interfaces.Services.Factories
{
    public interface IPedidoFactory
    {
        Pedido CriarPedido(string identificador, string descricao);
        ItemDoPedido AdicionarItem(int produtoId, int quantidade, double valorUnitario);
    }
}

