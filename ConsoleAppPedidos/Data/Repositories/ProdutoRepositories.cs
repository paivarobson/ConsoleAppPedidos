using ConsoleAppPedidos.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppPedidos.Data.Repositories
{
    /// <summary>
    /// Classe repositório para manipulação de dados da entidade Produto.
    /// </summary>
    public class ProdutoRepositories
    {
        /// <summary>
        /// Propriedade contexto do banco de dados usado para acessar os produtos.
        /// </summary>
        private readonly DBContexto dbContexto;

        /// <summary>
        /// Construtor da classe ProdutoRepositories.
        /// </summary>
        /// <param name="dbContexto">Contexto do banco de dados.</param>
        public ProdutoRepositories(DBContexto dbContexto)
        {
            this.dbContexto = dbContexto;
        }

        /// <summary>
        /// Método para criar um novo produto.
        /// </summary>
        /// <param name="produto">Produto que será criado.</param>
        public void CriarProduto(Produto produto)
        {
            dbContexto.Produtos.Add(produto);

            dbContexto.SaveChanges();
        }

        /// <summary>
        /// Método para carregar todos os produtos.
        /// </summary>
        /// <returns>Retorna todos os produtos. Usado IQueryable para consulta ser realizado diretamente no banco de dados.</returns>
        public IQueryable<Produto> CarregarProdutos()
        {
            return dbContexto.Produtos.AsQueryable();
        }

        /// <summary>
        /// Método para carregar um produto.
        /// </summary>
        /// <param name="produtoId">ID do produto.</param>
        /// <returns>Retorna o produto encontrado ou null se não encontrado.</returns>
        public Produto CarregarProduto(int produtoId)
        {
            return dbContexto.Produtos.FirstOrDefault(p => p.ID == produtoId);
        }

        /// <summary>
        /// Método para alterar um produto existente.
        /// </summary>
        /// <param name="produto">Retorna o produto com as alterações.</param>
        public void AlterarProduto(Produto produto)
        {
            var produtoEncontrado = dbContexto.Produtos.FirstOrDefault(p => p.ID == produto.ID);

            if (produtoEncontrado != null)
            {
                produtoEncontrado.Nome = produto.Nome;
                produtoEncontrado.Categoria = produto.Categoria;

                dbContexto.SaveChanges();
            }
        }

        /// <summary>
        /// Método para excluir um produto.
        /// </summary>
        /// <param name="produto">Produto que será excluído.</param>
        public void ExcluirProduto(Produto produto)
        {
            dbContexto.Produtos.Remove(produto);

            dbContexto.SaveChanges();
        }
    }
}
