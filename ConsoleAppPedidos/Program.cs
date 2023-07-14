using ConsoleAppPedidos.Infrastructure;
using ConsoleAppPedidos.Infrastructure.Repositories;
using ConsoleAppPedidos.Interfaces;
using ConsoleAppPedidos.Interfaces.Infrastructure;
using ConsoleAppPedidos.Interfaces.Services;
using ConsoleAppPedidos.Services;
using Microsoft.Extensions.DependencyInjection;

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
                using (var serviceProvider = new ServiceCollection()
                .AddSingleton<IAppDbContexto, AppDbContexto>()
                .AddSingleton<IProdutoRepository, ProdutoRepository>()
                .AddSingleton<IProdutoService, ProdutoService>()
                .BuildServiceProvider())
                {
                    // Inicia a execução do programa exibindo o menu principal
                    var menu = new MenuOpcoes(serviceProvider.GetRequiredService<IProdutoService>());

                    menu.ExibirMenuPrincipal();
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
