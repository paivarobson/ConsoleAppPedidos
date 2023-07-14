using ConsoleAppPedidos.Models;

namespace ConsoleAppPedidos.Infrastructure.Repositories
{
    /// <summary>
    /// Classe repositório para manipulação de dados da entidade ItemDoPedido.
    /// </summary>
    public class ItemDoPedidoRepository
    {
		/// <summary>
	    /// Propriedade contexto do banco de dados usado para acessar os itens do pedido.
	    /// </summary>    
        private readonly AppDbContexto dbContexto;

        /// <summary>
        /// Construtor da classe ItemDoPedidoRepository.
        /// </summary>
        /// <param name="dbContexto">Contexto do banco de dados.</param>
        /// <exception cref="ArgumentNullException">Exceção lançada quando o dbContexto é nulo.</exception>
        public ItemDoPedidoRepository(AppDbContexto dbContexto)
        {
            this.dbContexto = dbContexto ?? throw new ArgumentNullException(nameof(dbContexto));
        }

        /// <summary>
        /// Método para consultar todos os itens do pedido.
        /// </summary>
        /// <param name="pedidoId">ID do pedido.</param>
        /// <returns>Retorna todos os itens do pedido solicitado. Usado IQueryable para consulta ser realizado diretamente no banco de dados.</returns>
        /// <exception cref="Exception">Exceção lançada caso ocorra um erro ao consultar os itens do pedido.</exception>
        public IQueryable<ItemDoPedido> ConsultarItensDoPedido(int pedidoId)
        {
            try
            {
                return dbContexto.ItensDePedido.Where(i => i.PedidoID == pedidoId).AsQueryable();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao consultar os itens do pedido.", ex);
            }
        }

        /// <summary>
        /// Consulta todos os itens de pedido.
        /// </summary>
        /// <returns>Retorna todos os itens de pedido. Usado IQueryable para consulta ser realizado diretamente no banco de dados.</returns>
        /// <exception cref="Exception">Exceção lançada caso ocorra um erro ao consultar todos os itens de pedido.</exception>
        public IQueryable<ItemDoPedido> ConsultarTodosItensDePedido()
        {
            try
            {
                return dbContexto.ItensDePedido.AsQueryable();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao consultar todos os itens de pedido.", ex);
            }
        }

        /// <summary>
        /// Adiciona um novo item ao pedido.
        /// </summary>
        /// <param name="pedidoId">ID do pedido.</param>
        /// <param name="novoItem">Novo item do pedido.</param>
        /// <exception cref="ArgumentNullException">Exceção lançada caso o novoItem seja nulo.</exception>
        /// <exception cref="ArgumentException">Exceção lançada caso o pedido não exista ou caso já exista um item com o mesmo ID.</exception>
        /// <exception cref="InvalidOperationException">Exceção lançada caso ocorra uma operação inválida, como adicionar um item duplicado.</exception>
        /// <exception cref="Exception">Exceção lançada caso ocorra um erro ao adicionar o item ao pedido.</exception>
        public void AdicionarItemAoPedido(int pedidoId, ItemDoPedido novoItem)
        {
            try
            {
                if (novoItem == null)
                {
                    throw new ArgumentNullException(nameof(novoItem), "O item do pedido não pode ser nulo.");
                }

                var pedidoEncontrado = dbContexto.Pedidos.FirstOrDefault(p => p.ID == pedidoId);
                if (pedidoEncontrado == null)
                {
                    throw new ArgumentException("O pedido não existe.", nameof(pedidoId));
                }

                var itemExistente = dbContexto.ItensDePedido.Any(i => i.ID == novoItem.ID);
                if (itemExistente)
                {
                    throw new InvalidOperationException("Já existe um item com o mesmo ID.");
                }

                novoItem.PedidoID = pedidoId;
                dbContexto.ItensDePedido.Add(novoItem);
                SalvarItemPedido();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao adicionar o item ao pedido.", ex);
            }
        }

        /// <summary>
        /// Salva as alterações feitas em um item do pedido no banco de dados.
        /// </summary>
        /// <exception cref="Exception">Exceção lançada quando ocorre um erro ao salvar o item do pedido no banco de dados.</exception>
        private void SalvarItemPedido()
        {
            try
            {
                dbContexto.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o item do pedido no banco de dados.", ex);
            }
        }

        /// <summary>
        /// Altera um item do pedido.
        /// </summary>
        /// <param name="pedidoId">ID do pedido.</param>
        /// <param name="itemDePedidoId">ID do item do pedido a ser alterado.</param>
        /// <param name="itemDePedidoAtualizado">Item do pedido atualizado.</param>
        /// <exception cref="ArgumentNullException">Exceção lançada caso o itemDePedidoAtualizado seja nulo.</exception>
        /// <exception cref="ArgumentException">Exceção lançada caso o item do pedido não exista ou não esteja associado ao pedido especificado.</exception>
        /// <exception cref="Exception">Exceção lançada caso ocorra algum erro ao alterar o item do pedido.</exception>
        public void AlterarItemDoPedido(int pedidoId, int itemDePedidoId, ItemDoPedido itemDePedidoAtualizado)
        {
            try
            {
                if (itemDePedidoAtualizado == null)
                {
                    throw new ArgumentNullException(nameof(itemDePedidoAtualizado), "O item do pedido atualizado não pode ser nulo.");
                }

                var itemEncontrado = dbContexto.ItensDePedido.FirstOrDefault(i => i.ID == itemDePedidoId && i.PedidoID == pedidoId);
                if (itemEncontrado == null)
                {
                    throw new ArgumentException("O item do pedido não existe ou não está associado ao pedido especificado.", nameof(itemDePedidoId));
                }

                dbContexto.Entry(itemEncontrado).CurrentValues.SetValues(itemDePedidoAtualizado);
                SalvarItemPedido();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar o item do pedido.", ex);
            }
        }

        /// <summary>
        /// Exclui um item do pedido.
        /// </summary>
        /// <param name="itemDoPedido">Item do pedido a ser excluído.</param>
        /// <exception cref="ArgumentNullException">Exceção lançada caso o itemDoPedido seja nulo.</exception>
        /// <exception cref="Exception">Exceção lançada caso ocorra um erro ao excluir o item do pedido.</exception>
        public void ExcluirItemDoPedido(ItemDoPedido itemDoPedido)
        {
            try
            {
                if (itemDoPedido == null)
                {
                    throw new ArgumentNullException(nameof(itemDoPedido), "O item do pedido não pode ser nulo.");
                }

                dbContexto.ItensDePedido.Remove(itemDoPedido);
                SalvarItemPedido();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao excluir o item do pedido.", ex);
            }
        }

        /// <summary>
        /// Verifica se um produto está associado a algum pedido.
        /// </summary>
        /// <param name="produtoId">ID do produto a ser verificado.</param>
        /// <returns>True se o produto está associado a algum pedido, False caso contrário.</returns>
        /// <exception cref="Exception">Exceção lançada caso ocorra um erro ao verificar se o produto está associado a algum pedido.</exception>
        public bool ProdutoAssociadoPedido(int produtoId)
        {
            try
            {
                return dbContexto.ItensDePedido.Any(i => i.ProdutoID == produtoId);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao verificar se o produto está associado a algum pedido.", ex);
            }
        }
    }
}
