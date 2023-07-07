﻿using System;
using ConsoleAppPedidos.Models;

namespace ConsoleAppPedidos.Data.Repositories
{
    public class ItemDoPedidoRepositories
    {
        /// <summary>
        /// Propriedade contexto do banco de dados usado para acessar os itens do pedido.
        /// </summary>
        private readonly DBContexto dbContexto;

        /// <summary>
        /// Construtor da classe ItemDoPedidoRepositories.
        /// </summary>
        /// <param name="dbContexto">Contexto do banco de dados.</param>
        public ItemDoPedidoRepositories(DBContexto dbContexto)
        {
            this.dbContexto = dbContexto;
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
    }
}

