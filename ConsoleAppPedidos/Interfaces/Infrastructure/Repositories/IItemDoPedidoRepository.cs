using ConsoleAppPedidos.Models;

namespace ConsoleAppPedidos.Interfaces.Infrastructure.Repositories
{
    public interface IItemDoPedidoRepository
    {
        /// <summary>
        /// Método para consultar todos os itens do pedido.
        /// </summary>
        /// <param name="pedidoId">ID do pedido.</param>
        /// <returns>Retorna todos os itens do pedido solicitado. Usado IQueryable para consulta ser realizado diretamente no banco de dados.</returns>
        /// <exception cref="Exception">Exceção lançada caso ocorra um erro ao consultar os itens do pedido.</exception>
        IQueryable<ItemDoPedido> ConsultarItensDoPedido(int pedidoId);

        /// <summary>
        /// Consulta todos os itens de pedido.
        /// </summary>
        /// <returns>Retorna todos os itens de pedido. Usado IQueryable para consulta ser realizado diretamente no banco de dados.</returns>
        /// <exception cref="Exception">Exceção lançada caso ocorra um erro ao consultar todos os itens de pedido.</exception>
        IQueryable<ItemDoPedido> ConsultarTodosItensDePedido();

        /// <summary>
        /// Adiciona um novo item ao pedido.
        /// </summary>
        /// <param name="pedidoId">ID do pedido.</param>
        /// <param name="novoItem">Novo item do pedido.</param>
        /// <exception cref="ArgumentNullException">Exceção lançada caso o novoItem seja nulo.</exception>
        /// <exception cref="ArgumentException">Exceção lançada caso o pedido não exista ou caso já exista um item com o mesmo ID.</exception>
        /// <exception cref="InvalidOperationException">Exceção lançada caso ocorra uma operação inválida, como adicionar um item duplicado.</exception>
        /// <exception cref="Exception">Exceção lançada caso ocorra um erro ao adicionar o item ao pedido.</exception>
        void AdicionarItemAoPedido(int pedidoId, ItemDoPedido novoItem);

        /// <summary>
        /// Salva as alterações feitas em um item do pedido no banco de dados.
        /// </summary>
        /// <exception cref="Exception">Exceção lançada quando ocorre um erro ao salvar o item do pedido no banco de dados.</exception>
        void SalvarItemPedido();

        /// <summary>
        /// Altera um item do pedido.
        /// </summary>
        /// <param name="pedidoId">ID do pedido.</param>
        /// <param name="itemDePedidoId">ID do item do pedido a ser alterado.</param>
        /// <param name="itemDePedidoAtualizado">Item do pedido atualizado.</param>
        /// <exception cref="ArgumentNullException">Exceção lançada caso o itemDePedidoAtualizado seja nulo.</exception>
        /// <exception cref="ArgumentException">Exceção lançada caso o item do pedido não exista ou não esteja associado ao pedido especificado.</exception>
        /// <exception cref="Exception">Exceção lançada caso ocorra algum erro ao alterar o item do pedido.</exception>
        void AlterarItemDoPedido(int pedidoId, int itemDePedidoId, ItemDoPedido itemDePedidoAtualizado);

        /// <summary>
        /// Exclui um item do pedido.
        /// </summary>
        /// <param name="itemDoPedido">Item do pedido a ser excluído.</param>
        /// <exception cref="ArgumentNullException">Exceção lançada caso o itemDoPedido seja nulo.</exception>
        /// <exception cref="Exception">Exceção lançada caso ocorra um erro ao excluir o item do pedido.</exception>
        void ExcluirItemDoPedido(ItemDoPedido itemDoPedido);

        /// <summary>
        /// Verifica se um produto está associado a algum pedido.
        /// </summary>
        /// <param name="produtoId">ID do produto a ser verificado.</param>
        /// <returns>True se o produto está associado a algum pedido, False caso contrário.</returns>
        /// <exception cref="Exception">Exceção lançada caso ocorra um erro ao verificar se o produto está associado a algum pedido.</exception>
        bool ProdutoAssociadoPedido(int produtoId);
    }
}

