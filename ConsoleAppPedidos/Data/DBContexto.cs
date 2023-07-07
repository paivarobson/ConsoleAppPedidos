using ConsoleAppPedidos.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppPedidos.Data
{
    /// <summary>
    /// Classe que configura o Banco de Dados
    /// </summary>
    public class DBContexto : DbContext
    {
        /// <summary>
        /// Método que configura a conexão com o Banco de Dados SQL Server.
        /// </summary>
        /// <param name="optionsBuilder">Parâmetro para configuração do BD.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //String de conexão com Banco de Dados SQL Server
            optionsBuilder.UseSqlServer("Server=localhost;Database=DBConsoleAppPedidos;User ID=SA;Password=Password123;Encrypt=false;TrustServerCertificate=false;Connection Timeout=30;");
        }

        /// <summary>
        /// Propriedade que representará a tabela Produtos do BD.
        /// </summary>
        public DbSet<Produto> Produtos { get; set; }
        /// <summary>
        /// Propriedade que representará a tabela Pedidos do BD.
        /// </summary>
        public DbSet<Pedido> Pedidos { get; set; }
        /// <summary>
        /// Propriedade que representará a tabela ItensDePedido do BD.
        /// </summary>
        public DbSet<ItemDoPedido> ItensDePedido { get; set; }
    }
}