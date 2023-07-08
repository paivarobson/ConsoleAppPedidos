using ConsoleAppPedidos.Models;

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
        private readonly DBContexto dbContexto;

        /// <summary>
        /// Construtor da classe PedidoRepositories.
        /// </summary>
        /// <param name="dbContexto">Contexto do banco de dados.</param>
        public PedidoRepository(DBContexto dbContexto)
        {
            this.dbContexto = dbContexto;
        }

        /// <summary>
        /// Método para criar um novo pedido.
        /// </summary>
        /// <param name="pedido">O pedido que será criado.</param>
        public void CriarPedido(Pedido pedido)
        {
            dbContexto.Pedidos.Add(pedido);

            dbContexto.SaveChanges();
        }

        /// <summary>
        /// Método para carregar todos os pedidos.
        /// </summary>
        /// <returns>Retorna todos os pedidos. Usado IQueryable para consulta ser realizado diretamente no banco de dados.</returns>
        public IQueryable<Pedido> CarregarPedidos()
        {
            return dbContexto.Pedidos.AsQueryable();
        }

        /// <summary>
        /// Método para carregar um pedido com base no ID.
        /// </summary>
        /// <param name="pedidoId">O ID do pedido que será carregado.</param>
        /// <returns>Retorna o pedido ou null se não encontrado.</returns>
        public Pedido CarregarPedido(int pedidoId)
        {
            return dbContexto.Pedidos.FirstOrDefault(p => p.ID == pedidoId);
        }

        /// <summary>
        /// Método para alterar um pedido existente.
        /// </summary>
        /// <param name="pedido">O pedido que será atualizado.</param>
        public void AlterarPedido(Pedido pedido)
        {
            var pedidoEncontrado = dbContexto.Pedidos.FirstOrDefault(p => p.ID == pedido.ID);

            if (pedidoEncontrado != null)
            {
                dbContexto.Entry(pedidoEncontrado).CurrentValues.SetValues(pedido);

                dbContexto.SaveChanges();
            }
        }

        /// <summary>
        /// Método para excluir um pedido.
        /// </summary>
        /// <param name="pedido">O pedido que será excluído.</param>
        public void ExcluirPedido(Pedido pedido)
        {
            dbContexto.Pedidos.Remove(pedido);

            dbContexto.SaveChanges();
        }
    }
}
