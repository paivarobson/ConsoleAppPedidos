using ConsoleAppPedidos.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppPedidos.Data
{
    /// <summary>
    /// Classe que configura o contexto do banco de dados.
    /// </summary>
    public class AppDbContexto : DbContext
    {
        /// <summary>
        /// Propriedade que representa a tabela Produtos do banco de dados.
        /// </summary>
        public DbSet<Produto> Produtos { get; set; }

        /// <summary>
        /// Propriedade que representa a tabela Pedidos do banco de dados.
        /// </summary>
        public DbSet<Pedido> Pedidos { get; set; }

        /// <summary>
        /// Propriedade que representa a tabela ItensDePedido do banco de dados.
        /// </summary>
        public DbSet<ItemDoPedido> ItensDePedido { get; set; }

        /// <summary>
        /// Método que configura o contexto do banco de dados.
        /// </summary>
        /// <param name="optionsBuilder">Parâmetro para configuração do contexto.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Server=localhost;Database=DBConsoleAppPedidos;User ID=SA;Password=Password123;Encrypt=false;TrustServerCertificate=false;Connection Timeout=30;";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
