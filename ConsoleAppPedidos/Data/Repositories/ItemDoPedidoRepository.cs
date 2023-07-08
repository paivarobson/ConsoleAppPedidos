using System;
using System.Linq;
using ConsoleAppPedidos.Models;

namespace ConsoleAppPedidos.Data.Repositories
{
    public class ItemDoPedidoRepository
    {
        /// <summary>
        /// Propriedade contexto do banco de dados usado para acessar os itens do pedido.
        /// </summary>
        private readonly AppDbContexto dbContexto;

        /// <summary>
        /// Construtor da classe ItemDoPedidoRepositories.
        /// </summary>
        /// <param name="dbContexto">Contexto do banco de dados.</param>
        public ItemDoPedidoRepository(AppDbContexto dbContexto)
        {
            this.dbContexto = dbContexto;
        }

        /// <summary>
        /// Método para consultar todos os itens do pedido.
        /// </summary>
        /// <returns>Retorna todos os itens do pedido solicitado. Usado IQueryable para consulta ser realizado diretamente no banco de dados.</returns>
        public IQueryable<ItemDoPedido> ConsultarItensDoPedido(int pedidoId)
        {
            return dbContexto.ItensDePedido.Where(i => i.PedidoID == pedidoId).AsQueryable();
        }

        /// <summary>
        /// Método para consultar todos os itens de pedido.
        /// </summary>
        /// <returns>Retorna todos os itens de pedido. Usado IQueryable para consulta ser realizado diretamente no banco de dados.</returns>
        public IQueryable<ItemDoPedido> ConsultarTodosItensDePedido()
        {
            return dbContexto.ItensDePedido.AsQueryable();
        }

        /// <summary>
        /// Método para adicionar um novo item ao pedido.
        /// </summary>
        /// <param name="pedidoId">ID do pedido.</param>
        /// <param name="novoItem">O novo item que será adicionado.</param>
        public void AdicionarItemAoPedido(int pedidoId, ItemDoPedido novoItem)
        {
            var pedidoEncontrado = dbContexto.Pedidos.FirstOrDefault(p => p.ID == pedidoId);

            if (pedidoEncontrado != null)
            {
                var itemExistente = dbContexto.ItensDePedido.Any(i => i.ID == novoItem.ID);

                if (!itemExistente)
                {
                    novoItem.PedidoID = pedidoId;
                    dbContexto.ItensDePedido.Add(novoItem);

                    dbContexto.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método para alterar um item do pedido.
        /// </summary>
        /// <param name="pedidoId">ID do pedido.</param>
        /// <param name="itemDePedidoId">ID do item do pedido que será alterado.</param>
        /// <param name="itemDePedidoAtualizado">O item do pedido que será atualizado.</param>
        public void AlterarItemDoPedido(int pedidoId, int itemDePedidoId, ItemDoPedido itemDePedidoAtualizado)
        {
            var itemEncontrado = dbContexto.ItensDePedido.FirstOrDefault(i => i.ID == itemDePedidoId && i.PedidoID == pedidoId);

            if (itemEncontrado != null)
            {
                dbContexto.Entry(itemEncontrado).CurrentValues.SetValues(itemDePedidoAtualizado);

                dbContexto.SaveChanges();
            }
        }

        /// <summary>
        /// Método para excluir um item do pedido.
        /// </summary>
        /// <param name="itemDoPedido">O item do pedido que será excluído.</param>
        public void ExcluirItemDoPedido(ItemDoPedido itemDoPedido)
        {
            dbContexto.ItensDePedido.Remove(itemDoPedido);

            dbContexto.SaveChanges();
        }

        /// <summary>
        /// Método para verificar se um produto está associado a algum pedido.v
        /// </summary>
        /// <param name="produtoId">O ID do produto a ser verificado.</param>
        /// <returns>Retorna True se o produto estiver associado a algum pedido, False caso contrário.</returns>
        public bool ProdutoAssociadoPedido(int produtoId)
        {
            return dbContexto.ItensDePedido.Any(i => i.ProdutoID == produtoId);
        }
    }
}

