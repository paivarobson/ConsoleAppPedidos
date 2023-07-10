using ConsoleAppPedidos.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppPedidos.Data.Repositories
{
    /// <summary>
    /// Classe repositório para manipulação de dados da entidade Pedido.
    /// </summary>
    public class PedidoRepository
    {
        /// <summary>
        /// Propriedade contexto do banco de dados usado para acessar os pedidos.
        /// </summary>
        private readonly AppDbContexto dbContexto;

        /// <summary>
        /// Construtor da classe PedidoRepository.
        /// </summary>
        /// <param name="dbContexto">Contexto do banco de dados.</param>
        /// <exception cref="ArgumentNullException">Exceção lançada quando o dbContexto é nulo.</exception>
        public PedidoRepository(AppDbContexto dbContexto)
        {
            this.dbContexto = dbContexto ?? throw new ArgumentNullException(nameof(dbContexto));
        }

        /// <summary>
        /// Cria um novo pedido.
        /// </summary>
        /// <param name="pedido">O pedido a ser criado.</param>
        /// <exception cref="Exception">Exceção lançada quando ocorre um erro ao criar o pedido no banco de dados.</exception>
        public void CriarPedido(Pedido pedido)
        {
            try
            {
                dbContexto.Pedidos.Add(pedido);
                SalvarPedido();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao criar o pedido no banco de dados.", ex);
            }
        }

        public void SalvarPedido()
        {
            try
            {
                dbContexto.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o pedido no banco de dados.", ex);
            }
        }

        /// <summary>
        /// Consulta todos os pedidos.
        /// </summary>
        /// <returns>Uma consulta de todos os pedidos.</returns>
        public IQueryable<Pedido> ConsultarTodosPedidos()
        {
            return dbContexto.Pedidos.AsQueryable();
        }

        /// <summary>
        /// Consulta um pedido pelo ID.
        /// </summary>
        /// <param name="pedidoId">O ID do pedido.</param>
        /// <returns>O pedido encontrado ou null se não encontrado.</returns>
        public Pedido ConsultarPedido(int pedidoId)
        {
            return dbContexto.Pedidos.FirstOrDefault(p => p.ID == pedidoId);
        }

        /// <summary>
        /// Altera um pedido existente.
        /// </summary>
        /// <param name="pedido">O pedido com as alterações.</param>
        /// <exception cref="Exception">Exceção lançada quando ocorre um erro ao alterar o pedido no banco de dados.</exception>
        public void AlterarPedido(Pedido pedido)
        {
            try
            {
                var pedidoEncontrado = dbContexto.Pedidos.FirstOrDefault(p => p.ID == pedido.ID);

                if (pedidoEncontrado == null)
                    throw new InvalidOperationException($"Pedido com ID {pedido.ID} não encontrado.");

                dbContexto.Entry(pedidoEncontrado).CurrentValues.SetValues(pedido);
                dbContexto.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar o pedido no banco de dados.", ex);
            }
        }

        /// <summary>
        /// Exclui um pedido.
        /// </summary>
        /// <param name="pedido">O pedido a ser excluído.</param>
        /// <exception cref="Exception">Exceção lançada quando ocorre um erro ao excluir o pedido no banco de dados.</exception>
        public void ExcluirPedido(Pedido pedido)
        {
            try
            {
                var pedidoEncontrado = dbContexto.Pedidos.FirstOrDefault(p => p.ID == pedido.ID);

                if (pedidoEncontrado == null)
                    throw new InvalidOperationException($"Pedido com ID {pedido.ID} não encontrado.");

                dbContexto.Pedidos.Remove(pedidoEncontrado);
                dbContexto.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao excluir o pedido no banco de dados.", ex);
            }
        }

        /// <summary>
        /// Consulta o último pedido cadastrado.
        /// </summary>
        /// <returns>O último pedido cadastrado ou null se não houver pedidos.</returns>
        public Pedido ConsultarUltimoPedido()
        {
            var ultimoPedido = dbContexto.Pedidos.OrderByDescending(p => p.ID).FirstOrDefault();

            if (ultimoPedido == null)
                throw new InvalidOperationException("Não há pedidos cadastrados.");

            return ultimoPedido;
        }
    }
}
