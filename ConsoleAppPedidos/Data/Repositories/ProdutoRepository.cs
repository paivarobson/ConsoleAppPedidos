using ConsoleAppPedidos.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppPedidos.Data.Repositories
{
    /// <summary>
    /// Classe repositório para manipulação de dados da entidade Produto.
    /// </summary>
    public class ProdutoRepository
    {
        /// <summary>
        /// Propriedade contexto do banco de dados usado para acessar os produtos.
        /// </summary>
        private readonly AppDbContexto dbContexto;

        /// <summary>
        /// Construtor da classe ProdutoRepository.
        /// </summary>
        /// <param name="dbContexto">Contexto do banco de dados.</param>
        public ProdutoRepository(AppDbContexto dbContexto)
        {
            this.dbContexto = dbContexto;
        }

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="produto">O produto a ser criado.</param>
        public void CriarProduto(Produto produto)
        {
            dbContexto.Produtos.Add(produto);
            dbContexto.SaveChanges();
        }

        /// <summary>
        /// Consulta todos os produtos.
        /// </summary>
        /// <returns>Uma lista de produtos.</returns>
        public IQueryable<Produto> ConsultarProdutos()
        {
            return dbContexto.Produtos.AsQueryable();
        }

        /// <summary>
        /// Consulta um produto pelo ID.
        /// </summary>
        /// <param name="produtoId">O ID do produto.</param>
        /// <returns>O produto encontrado ou null se não encontrado.</returns>
        public Produto ConsultarProduto(int produtoId)
        {
            return dbContexto.Produtos.FirstOrDefault(p => p.ID == produtoId);
        }

        /// <summary>
        /// Altera um produto existente.
        /// </summary>
        /// <param name="produto">O produto com as alterações.</param>
        /// <returns>True se o produto foi alterado com sucesso, False caso contrário.</returns>
        public bool AlterarProduto(Produto produto)
        {
            var produtoEncontrado = dbContexto.Produtos.FirstOrDefault(p => p.ID == produto.ID);

            if (produtoEncontrado != null)
            {
                produtoEncontrado.Nome = produto.Nome;
                produtoEncontrado.Categoria = produto.Categoria;

                dbContexto.SaveChanges();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Exclui um produto.
        /// </summary>
        /// <param name="produto">O produto a ser excluído.</param>
        public void ExcluirProduto(Produto produto)
        {
            dbContexto.Produtos.Remove(produto);
            dbContexto.SaveChanges();
        }
    }
}
