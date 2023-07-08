﻿using ConsoleAppPedidos.Services;

namespace ConsoleAppPedidos
{
    /// <summary>
    /// Classe responsável por exibir e gerenciar o menu principal do sistema.
    /// </summary>
    public class MenuOpcoes
    {
        /// <summary>
        /// Exibe o menu principal e permite a seleção de opções.
        /// </summary>
        public static void ExibirMenuPrincipal()
        {
            Console.WriteLine("MENU PRINCIPAL:");
            Console.WriteLine("1. PRODUTO");
            Console.WriteLine("2. PEDIDO");
            Console.WriteLine("0. Sair");

            Console.Write("Selecione uma opção: ");
            string opcao = Console.ReadLine();

            Console.Clear();

            switch (opcao)
            {
                case "1":
                    MenuProduto();
                    break;
                case "2":
                    MenuPedido();
                    break;
                case "0":
                    Console.WriteLine("Encerrando o programa...");
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            Console.WriteLine("Aperte Enter para voltar ao MENU PRINCIPAL");
            Console.Read();

            Console.Clear();

            ExibirMenuPrincipal();
        }

        /// <summary>
        /// Exibe o menu de opções relacionadas a pedidos e permite a seleção de opções.
        /// </summary>
        private static void MenuPedido()
        {
            PedidoService pedidoService = new PedidoService();

            Console.WriteLine("Opções do CRUD:");
            Console.WriteLine("1. Criar um novo pedido");
            Console.WriteLine("2. Consultar um pedido existente");
            Console.WriteLine("3. Alterar um pedido existente");
            Console.WriteLine("4. Excluir um pedido existente");
            Console.WriteLine("9. Voltar ao MENU PRINCIPAL");
            Console.WriteLine("0. Sair");

            Console.Write("Selecione uma opção: ");
            string opcao = Console.ReadLine();

            Console.Clear();

            switch (opcao)
            {
                case "1":
                    pedidoService.CriarPedido();
                    break;
                case "2":
                    pedidoService.ConsultarPedido();
                    break;
                case "3":
                    pedidoService.AlterarPedido();
                    break;
                case "4":
                    pedidoService.ExcluirPedido();
                    break;
                case "9":
                    ExibirMenuPrincipal();
                    break;
                case "0":
                    Console.WriteLine("Encerrando o programa...");
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            Console.WriteLine("Aperte Enter para voltar ao MENU PEDIDO");
            Console.Read();

            Console.Clear();

            MenuPedido();
        }

        /// <summary>
        /// Exibe o menu de opções relacionadas a produtos e permite a seleção de opções.
        /// </summary>
        private static void MenuProduto()
        {
            ProdutoService produtoService = new ProdutoService();

            Console.WriteLine("Opções do CRUD:");
            Console.WriteLine("1. Criar um novo produto");
            Console.WriteLine("2. Consultar um produto existente");
            Console.WriteLine("3. Alterar um produto existente");
            Console.WriteLine("4. Excluir um produto existente");
            Console.WriteLine("9. Voltar ao MENU PRINCIPAL");
            Console.WriteLine("0. Sair");

            Console.Write("Selecione uma opção: ");
            string opcao = Console.ReadLine();

            Console.Clear();

            switch (opcao)
            {
                case "1":
                    produtoService.CriarProduto();
                    break;
                case "2":
                    produtoService.ConsultarProduto();
                    break;
                case "3":
                    produtoService.AlterarProduto();
                    break;
                case "4":
                    produtoService.ExcluirProduto();
                    break;
                case "9":
                    ExibirMenuPrincipal();
                    break;
                case "0":
                    Console.WriteLine("Encerrando o programa...");
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            Console.WriteLine("Aperte Enter para voltar ao MENU PRODUTO");
            Console.Read();

            Console.Clear();

            MenuProduto();
        }
    }
}
