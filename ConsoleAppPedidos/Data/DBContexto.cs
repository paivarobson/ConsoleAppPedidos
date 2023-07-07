using ConsoleAppPedidos.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppPedidos.Data
{
    public class DBContexto : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=DBConsoleAppPedidos;User ID=SA;Password=Password123;Encrypt=false;TrustServerCertificate=false;Connection Timeout=30;");
        }

        public DbSet<Produto> Produto { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<ItensDePedido> ItensDePedido { get; set; }
    }
}