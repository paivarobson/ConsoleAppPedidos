using ConsoleAppPedidos.Interfaces.Infrastructure.Data;
using ConsoleAppPedidos.Interfaces.Infrastructure.Repositories;
using ConsoleAppPedidos.Models;

namespace ConsoleAppPedidos.Infrastructure.Repositories
{
    /// <summary>
    /// Classe de repositório para manipulação de dados da entidade Produto.
    /// </summary>
    public class ProdutoRepository : IProdutoRepository
    {
        /// <summary>
        /// Propriedade contexto do banco de dados usado para acessar os produtos.
        /// </summary>
        private readonly IAppDbContexto dbContexto;

        /// <summary>
        /// Construtor da classe ProdutoRepository.
        /// </summary>
        /// <param name="dbContexto">Contexto do banco de dados.</param>
        /// <exception cref="ArgumentNullException">Exceção lançada quando o dbContexto é nulo.</exception>
        public ProdutoRepository(IAppDbContexto dbContexto)
        {
            try
            {
                this.dbContexto = dbContexto;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro no construtor do ProdutoRepository", ex);
            }
        }

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="produto">O produto a ser criado.</param>
        /// <exception cref="Exception">Exceção lançada quando ocorre um erro ao criar o produto no banco de dados.</exception>
        public void CriarProduto(Produto produto)
        {
            try
            {
                dbContexto.Produtos.Add(produto);
                SalvarProduto();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Salva as alterações feitas em um produto no banco de dados.
        /// </summary>
        /// <exception cref="Exception">Exceção lançada quando ocorre um erro ao salvar o produto no banco de dados.</exception>
        public void SalvarProduto()
        {
            try
            {
                dbContexto.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o produto no banco de dados.", ex);
            }
        }

        /// <summary>
        /// Consulta todos os produtos.
        /// </summary>
        /// <returns>Uma lista de produtos.</returns>
        public IEnumerable<Produto> ConsultarTodosProdutos()
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
        /// Verifica se um produto com o ID especificado existe no banco de dados.
        /// </summary>
        /// <param name="produtoId">ID do produto a ser verificado.</param>
        /// <returns>True se o produto existir, False caso contrário.</returns>
        /// <exception cref="Exception">Exceção lançada quando ocorre um erro ao consultar o produto no banco de dados.</exception>
        public bool ConsultarProdutoExiste(int produtoId)
        {
            try
            {
                return dbContexto.Produtos.Any(p => p.ID == produtoId);
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
                var produtoEncontrado = dbContexto.Produtos.Find(produto.ID);

                if (produtoEncontrado == null)
                    throw new InvalidOperationException($"Produto com ID {produto.ID} não encontrado.");

                produtoEncontrado.Descricao = produto.Descricao;
                produtoEncontrado.Categoria = produto.Categoria;

                SalvarProduto();

                return true;

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
                SalvarProduto();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao excluir o produto no banco de dados.", ex);
            }
        }
    }
}
