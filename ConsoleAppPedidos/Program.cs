using ConsoleAppPedidos.Infrastructure.Data;
using ConsoleAppPedidos.Infrastructure.Repositories;
using ConsoleAppPedidos.Interfaces.Infrastructure.Data;
using ConsoleAppPedidos.Interfaces.Infrastructure.Repositories;
using ConsoleAppPedidos.Interfaces.Services;
using ConsoleAppPedidos.Interfaces.Services.Factories;
using ConsoleAppPedidos.Services;
using ConsoleAppPedidos.Services.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppPedidos
{
    /// <summary>
    /// Classe principal que contém o método de entrada do programa.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Método principal que é executado quando o programa é iniciado.
        /// </summary>
        /// <param name="args">Argumentos de linha de comando (opcional).</param>
        static void Main(string[] args)
        {
            try
            {
                CriarAtualizarBancoDeDados();

                var serviceProvider = ConfigureServices();

                using (var escopo = serviceProvider.CreateScope())
                {
                    var menu = new MenuOpcoes(
                        escopo.ServiceProvider.GetRequiredService<IPedidoService>(),
                        escopo.ServiceProvider.GetRequiredService<IProdutoService>());

                    menu.ExibirMenuPrincipal();

                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro na inicialização do sistema: " + ex.Message);
            }
        }

        private static IServiceProvider ConfigureServices()
        {
            try
            {
                return new ServiceCollection()
                    .AddSingleton<IAppDbContexto, AppDbContexto>()
                    .AddSingleton<IPedidoFactory, PedidoFactory>()
                    .AddSingleton<IPedidoRepository, PedidoRepository>()
                    .AddSingleton<IItemDoPedidoRepository, ItemDoPedidoRepository>()
                    .AddSingleton<IPedidoService, PedidoService>()
                    .AddSingleton<IProdutoFactory, ProdutoFactory>()
                    .AddSingleton<IProdutoRepository, ProdutoRepository>()
                    .AddSingleton<IProdutoService, ProdutoService>()
                    .BuildServiceProvider();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static void CriarAtualizarBancoDeDados()
        {
            try
            {
                using (var dbContexto = new AppDbContexto())
                {
                    dbContexto.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
