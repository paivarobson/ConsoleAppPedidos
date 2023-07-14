using ConsoleAppPedidos.Models;

namespace ConsoleAppPedidos.Interfaces.Services
{
    /// <summary>
    /// Interface responsável por definir as operações relacionadas a pedidos.
    /// </summary>
    public interface IPedidoService
    {
        /// <summary>
        /// Cria um novo pedido.
        /// </summary>
        void CriarPedido();

        /// <summary>
        /// Consulta e imprime todos os pedidos cadastrados.
        /// </summary>
        /// <exception cref="InvalidOperationException">Exceção lançada quando ocorre um erro ao consultar os pedidos.</exception>
        /// <exception cref="Exception">Exceção genérica lançada quando ocorre um erro ao consultar ou imprimir os pedidos.</exception>
        void ConsultarTodosPedidos();

        /// <summary>
        /// Consulta um pedido pelo seu ID.
        /// </summary>
        /// <param name="pedidoId">ID do pedido a ser consultado (opcional).</param>
        void ConsultarPedido(int pedidoId = 0);

        /// <summary>
        /// Altera um pedido existente.
        /// </summary>
        void AlterarPedido();

        /// <summary>
        /// Exclui um pedido existente e seus itens associados.
        /// </summary>
        void ExcluirPedido();

        /// <summary>
        /// Gera um identificador único para um novo pedido no formato "P_[letra, seguida de 3 números]_C".
        /// </summary>
        /// <returns>O identificador gerado para o novo pedido.</returns>
        /// <exception cref="Exception">Exceção lançada quando ocorre um erro ao calcular o valor total do pedido no banco de dados.</exception>
        string GerarIdentificadorDoPedido();

        /// <summary>
        /// Calcula o valor total de um pedido, somando os valores unitários dos itens do pedido.
        /// </summary>
        /// <param name="pedidoId">O ID do pedido.</param>
        /// <returns>O valor total do pedido.</returns>
        /// <exception cref="Exception">Exceção lançada quando ocorre um erro ao calcular o valor total do pedido no banco de dados.</exception>
        double CalcularValorTotal(int pedidoId);

        /// <summary>
        /// Imprime os detalhes de um ou mais pedidos, incluindo seus itens.
        /// </summary>
        /// <param name="pedidos">A lista de pedidos a serem impressos.</param>
        /// <exception cref="InvalidOperationException">Exceção lançada quando ocorre um erro ao consultar o pedido ou seus itens, ou o pedido não é encontrado.</exception>
        /// <exception cref="Exception">Exceção genérica lançada quando ocorre um erro ao imprimir o pedido.</exception>
        void ImprimirPedido(List<Pedido> pedidos);
    }
}
