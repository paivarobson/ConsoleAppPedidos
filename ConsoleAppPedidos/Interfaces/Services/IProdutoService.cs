using ConsoleAppPedidos.Models;

namespace ConsoleAppPedidos.Interfaces.Services
{
    /// <summary>
    /// Interface responsável por definir as operações relacionadas a produtos.
    /// </summary>
    public interface IProdutoService
    {
        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        void CriarProduto();

        /// <summary>
        /// Consulta e imprime todos os produtos.
        /// </summary>
        void ConsultarTodosProdutos();

        /// <summary>
        /// Consulta um produto pelo seu ID.
        /// </summary>
        /// <param name="produtoId">ID do produto a ser consultado (opcional).</param>
        void ConsultarProduto(int produtoId = 0);

        /// <summary>
        /// Exclui um produto existente.
        /// </summary>
        void ExcluirProduto();

        /// <summary>
        /// Altera um produto existente.
        /// </summary>
        void AlterarProduto();

        /// <summary>
        /// Imprime os detalhes de um produto.
        /// </summary>
        /// <param name="listaProdutos">Lista de produtos a serem impressos.</param>
        /// <exception cref="InvalidOperationException">Exceção lançada quando ocorre um erro ao consultar o produto ou o produto não é encontrado.</exception>
        /// <exception cref="Exception">Exceção genérica lançada quando ocorre um erro ao imprimir o produto.</exception>
        void ImprimirProduto(List<Produto> listaProdutos);
    }
}
