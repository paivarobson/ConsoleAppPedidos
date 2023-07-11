using ConsoleAppPedidos.Models;
using System.Collections.Generic;

namespace ConsoleAppPedidos.Interfaces
{
    /// <summary>
    /// Interface responsável por definir as operações de acesso a dados relacionadas aos produtos.
    /// </summary>
    public interface IProdutoRepository
    {
        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="produto">O objeto Produto a ser criado.</param>
        void CriarProduto(Produto produto);

        /// <summary>
        /// Salva as alterações feitas no produto no repositório de dados.
        /// </summary>
        void SalvarProduto();

        /// <summary>
        /// Consulta todos os produtos cadastrados.
        /// </summary>
        /// <returns>Uma coleção de objetos Produto.</returns>
        IEnumerable<Produto> ConsultarTodosProdutos();

        /// <summary>
        /// Consulta um produto pelo seu ID.
        /// </summary>
        /// <param name="produtoId">O ID do produto a ser consultado.</param>
        /// <returns>O objeto Produto encontrado, ou null se não encontrado.</returns>
        Produto ConsultarProduto(int produtoId);

        /// <summary>
        /// Verifica se um produto com o ID informado existe no repositório de dados.
        /// </summary>
        /// <param name="produtoId">O ID do produto a ser verificado.</param>
        /// <returns>True se o produto existe, False caso contrário.</returns>
        bool ConsultarProdutoExiste(int produtoId);

        /// <summary>
        /// Altera as informações de um produto existente.
        /// </summary>
        /// <param name="produto">O objeto Produto com as informações atualizadas.</param>
        /// <returns>True se a alteração foi bem-sucedida, False caso contrário.</returns>
        bool AlterarProduto(Produto produto);

        /// <summary>
        /// Exclui um produto do repositório de dados.
        /// </summary>
        /// <param name="produto">O objeto Produto a ser excluído.</param>
        void ExcluirProduto(Produto produto);
    }
}
