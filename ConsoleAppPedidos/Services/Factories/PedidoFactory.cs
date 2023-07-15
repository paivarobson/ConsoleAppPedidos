using ConsoleAppPedidos.Interfaces.Services.Factories;
using ConsoleAppPedidos.Models;

namespace ConsoleAppPedidos.Services.Factories
{
    /// <summary>
    /// Classe responsável por criar pedidos e itens de pedido.
    /// </summary>
    public class PedidoFactory : IPedidoFactory
    {
        /// <summary>
        /// Cria um novo pedido com base no identificador e descrição fornecidos.
        /// </summary>
        /// <param name="identificador">O identificador do pedido.</param>
        /// <param name="descricao">A descrição do pedido.</param>
        /// <returns>O pedido criado.</returns>
        /// <exception cref="Exception">Exceção lançada quando ocorre um erro ao criar o pedido.</exception>
        public Pedido CriarPedido(string identificador, string descricao)
        {
            try
            {
                var novoPedido = new Pedido
                {
                    Identificador = identificador,
                    Descricao = descricao
                };

                return novoPedido;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao criar o pedido", ex);
            }
        }

        /// <summary>
        /// Cria um novo item de pedido com base no produto ID, quantidade e valor unitário fornecidos.
        /// </summary>
        /// <param name="produtoId">O ID do produto.</param>
        /// <param name="quantidade">A quantidade do item de pedido.</param>
        /// <param name="valorUnitario">O valor unitário do item de pedido.</param>
        /// <returns>O item de pedido criado.</returns>
        /// <exception cref="Exception">Exceção lançada quando ocorre um erro ao adicionar o item ao pedido.</exception>
        public ItemDoPedido AdicionarItem(int produtoId, int quantidade, double valorUnitario)
        {
            try
            {
                var novoItem = new ItemDoPedido
                {
                    ProdutoID = produtoId,
                    Quantidade = quantidade,
                    Valor = valorUnitario
                };

                return novoItem;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao adicionar item ao pedido", ex);
            }
        }
    }
}
