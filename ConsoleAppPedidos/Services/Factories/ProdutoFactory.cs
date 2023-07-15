using ConsoleAppPedidos.Interfaces.Services.Factories;
using ConsoleAppPedidos.Models;

namespace ConsoleAppPedidos.Services.Factories
{
    /// <summary>
    /// Classe responsável por criar produtos.
    /// </summary>
    public class ProdutoFactory : IProdutoFactory
    {
        /// <summary>
        /// Cria um novo produto com base na descrição e categoria fornecidas.
        /// </summary>
        /// <param name="descricao">A descrição do produto.</param>
        /// <param name="categoria">A categoria do produto.</param>
        /// <returns>O produto criado.</returns>
        /// <exception cref="Exception">Exceção lançada quando ocorre um erro ao criar o produto.</exception>
        public Produto CriarProduto(string descricao, int categoria)
        {
            try
            {
                var novoProduto = new Produto
                {
                    Descricao = descricao,
                    Categoria = categoria
                };

                return novoProduto;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao criar o produto.", ex);
            }
        }
    }
}
