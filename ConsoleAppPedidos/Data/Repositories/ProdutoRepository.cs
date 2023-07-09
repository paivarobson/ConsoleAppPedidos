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
        /// <exception cref="ArgumentNullException">Exceção lançada quando o dbContexto é nulo.</exception>
        public ProdutoRepository(AppDbContexto dbContexto)
        {
            this.dbContexto = dbContexto ?? throw new ArgumentNullException(nameof(dbContexto));
        }

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="produto">O produto a ser criado.</param>
        /// <exception cref="Exception">Ocorre quando há um erro ao criar o produto no banco de dados.</exception>
        public void CriarProduto(Produto produto)
        {
            try
            {
                dbContexto.Produtos.Add(produto);
                dbContexto.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao criar o produto no banco de dados.", ex);
            }
        }

        /// <summary>
        /// Consulta todos os produtos.
        /// </summary>
        /// <returns>Uma lista de produtos.</returns>
        /// <exception cref="Exception">Ocorre quando há um erro ao consultar os produtos no banco de dados.</exception>
        public IQueryable<Produto> ConsultarProdutos()
        {
            try
            {
                var produtosEncontrados = dbContexto.Produtos.AsQueryable();

                if (produtosEncontrados == null)
                    throw new InvalidOperationException("Nenhum produto encontrado.");

                return produtosEncontrados;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao consultar os produtos no banco de dados.", ex);
            }
        }

        /// <summary>
        /// Consulta um produto pelo ID.
        /// </summary>
        /// <param name="produtoId">O ID do produto.</param>
        /// <returns>O produto encontrado ou null se não encontrado.</returns>
        /// <exception cref="Exception">Ocorre quando há um erro ao consultar o produto no banco de dados.</exception>
        public Produto ConsultarProduto(int produtoId)
        {
            try
            {
                var produtoEncontrado = dbContexto.Produtos.FirstOrDefault(p => p.ID == produtoId);

                if (produtoEncontrado == null)
                    throw new InvalidOperationException($"Produto com ID {produtoId} não encontrado.");

                return produtoEncontrado;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao consultar o produto no banco de dados.", ex);
            }
        }

        /// <summary>
        /// Altera um produto existente.
        /// </summary>
        /// <param name="produto">O produto com as alterações.</param>
        /// <returns>True se o produto foi alterado com sucesso, False caso contrário.</returns>
        /// <exception cref="Exception">Ocorre quando há um erro ao alterar o produto no banco de dados.</exception>
        public bool AlterarProduto(Produto produto)
        {
            try
            {
                bool produtoAlterado = false;

                var produtoEncontrado = dbContexto.Produtos.FirstOrDefault(p => p.ID == produto.ID);

                if (produtoEncontrado == null)
                    throw new InvalidOperationException($"Produto com ID {produto.ID} não encontrado.");

                produtoEncontrado.Nome = produto.Nome;
                produtoEncontrado.Categoria = produto.Categoria;

                dbContexto.SaveChanges();

                return produtoAlterado;

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar o produto no banco de dados.", ex);
            }
        }

        /// <summary>
        /// Exclui um produto.
        /// </summary>
        /// <param name="produto">O produto a ser excluído.</param>
        /// <exception cref="Exception">Ocorre quando há um erro ao excluir o produto no banco de dados.</exception>
        public void ExcluirProduto(Produto produto)
        {
            try
            {
                dbContexto.Produtos.Remove(produto);
                dbContexto.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao excluir o produto no banco de dados.", ex);
            }
        }
    }
}
