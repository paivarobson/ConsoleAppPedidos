using ConsoleAppPedidos.Models;

namespace ConsoleAppPedidos.Interfaces.Services.Factories
{
    /// <summary>
    /// Interface responsável por definir um contrato para a criação de pedidos.
    /// </summary>
    public interface IPedidoFactory
    {
        /// <summary>
        /// Cria um novo pedido com base no identificador e descrição fornecidos.
        /// </summary>
        /// <param name="identificador">O identificador do pedido.</param>
        /// <param name="descricao">A descrição do pedido.</param>
        /// <returns>O pedido criado.</returns>
        Pedido CriarPedido(string identificador, string descricao);

        /// <summary>
        /// Adiciona um item ao pedido com base no ID do produto, quantidade e valor unitário fornecidos.
        /// </summary>
        /// <param name="produtoId">O ID do produto a ser adicionado.</param>
        /// <param name="quantidade">A quantidade do produto a ser adicionada.</param>
        /// <param name="valorUnitario">O valor unitário do produto a ser adicionado.</param>
        /// <returns>O item do pedido criado.</returns>
        ItemDoPedido AdicionarItem(int produtoId, int quantidade, double valorUnitario);
    }
}
