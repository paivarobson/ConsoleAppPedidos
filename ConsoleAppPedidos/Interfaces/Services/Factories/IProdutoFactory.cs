using ConsoleAppPedidos.Models;

namespace ConsoleAppPedidos.Interfaces.Services.Factories
{
    /// <summary>
    /// Interface responsável por definir um contrato para a criação de produtos.
    /// </summary>
    public interface IProdutoFactory
    {
        /// <summary>
        /// Cria um novo produto com base na descrição e categoria fornecidas.
        /// </summary>
        /// <param name="descricao">A descrição do produto.</param>
        /// <param name="categoria">A categoria do produto.</param>
        /// <returns>O produto criado.</returns>
        Produto CriarProduto(string descricao, int categoria);
    }
}
