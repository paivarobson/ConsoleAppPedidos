﻿using ConsoleAppPedidos.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppPedidos.Interfaces.Infrastructure.Data
{
    /// <summary>
    /// Interface que define o contexto do banco de dados da aplicação.
    /// </summary>
    public interface IAppDbContexto
    {
        /// <summary>
        /// Propriedade que representa a tabela Produtos do banco de dados.
        /// </summary>
        DbSet<Produto> Produtos { get; set; }

        /// <summary>
        /// Propriedade que representa a tabela Pedidos do banco de dados.
        /// </summary>
        DbSet<Pedido> Pedidos { get; set; }

        /// <summary>
        /// Propriedade que representa a tabela ItensDePedido do banco de dados.
        /// </summary>
        DbSet<ItemDoPedido> ItensDePedido { get; set; }

        /// <summary>
        /// Salva as alterações feitas no banco de dados.
        /// </summary>
        /// <returns>O número de registros afetados.</returns>
        int SaveChanges();
    }
}