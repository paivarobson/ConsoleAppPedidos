using ConsoleAppPedidos.Interfaces.Infrastructure.Data;
using ConsoleAppPedidos.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppPedidos.Infrastructure.Data
{
    /// <summary>
    /// Classe que implementa o contexto do banco de dados da aplicação.
    /// </summary>
    public class AppDbContexto : DbContext, IAppDbContexto
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
        /// <exception cref="Exception">Exceção lançada quando ocorre um erro ao configurar o contexto do banco de dados.</exception>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                if (!optionsBuilder.IsConfigured)
                {
                    string connectionString = "Server=localhost;Database=DBConsoleAppPedidos;User ID=SA;Password=Password123;Encrypt=false;TrustServerCertificate=false;Connection Timeout=5;";
                    optionsBuilder.UseSqlServer(connectionString);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao configurar o contexto do banco de dados.", ex);
            }
        }
    }
}
