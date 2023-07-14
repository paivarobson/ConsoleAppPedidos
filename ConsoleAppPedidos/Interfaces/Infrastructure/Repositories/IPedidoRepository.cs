using ConsoleAppPedidos.Models;

namespace ConsoleAppPedidos.Interfaces.Infrastructure
{
    /// <summary>
    /// Interface responsável por definir as operações de acesso a dados relacionadas a pedidos.
    /// </summary>
    public interface IPedidoRepository
    {
        /// <summary>
        /// Cria um novo pedido.
        /// </summary>
        /// <param name="pedido">O pedido a ser criado.</param>
        /// <exception cref="Exception">Exceção lançada quando ocorre um erro ao criar o pedido no banco de dados.</exception>
        void CriarPedido(Pedido pedido);

        /// <summary>
        /// Salva as alterações feitas em um pedido no banco de dados.
        /// </summary>
        /// <exception cref="Exception">Exceção lançada quando ocorre um erro ao salvar o pedido no banco de dados.</exception>
        void SalvarPedido();

        /// <summary>
        /// Consulta todos os pedidos.
        /// </summary>
        /// <returns>Uma consulta de todos os pedidos.</returns>
        IQueryable<Pedido> ConsultarTodosPedidos();

        /// <summary>
        /// Consulta um pedido pelo ID.
        /// </summary>
        /// <param name="pedidoId">O ID do pedido.</param>
        /// <returns>O pedido encontrado ou null se não encontrado.</returns>
        Pedido ConsultarPedido(int pedidoId);

        /// <summary>
        /// Altera um pedido existente.
        /// </summary>
        /// <param name="pedido">O pedido com as alterações.</param>
        /// <exception cref="Exception">Exceção lançada quando ocorre um erro ao alterar o pedido no banco de dados.</exception>
        void AlterarPedido(Pedido pedido);

        /// <summary>
        /// Exclui um pedido.
        /// </summary>
        /// <param name="pedido">O pedido a ser excluído.</param>
        /// <exception cref="Exception">Exceção lançada quando ocorre um erro ao excluir o pedido no banco de dados.</exception>
        void ExcluirPedido(Pedido pedido);

        /// <summary>
        /// Consulta o último pedido cadastrado.
        /// </summary>
        /// <returns>O último pedido cadastrado ou null se não houver pedidos.</returns>
        Pedido ConsultarUltimoPedido();
    }
}
