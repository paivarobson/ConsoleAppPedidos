using ConsoleAppPedidos.Interfaces.Services;
using ConsoleAppPedidos.Services;

namespace ConsoleAppPedidos
{
    /// <summary>
    /// Classe responsável por exibir e gerenciar o menu principal do sistema.
    /// </summary>
    public class MenuOpcoes
    {
        private readonly IPedidoService pedidoService;
        private readonly IProdutoService produtoService;

        public MenuOpcoes(IPedidoService pedidoService, IProdutoService produtoService)
        {
            this.pedidoService = pedidoService;
            this.produtoService = produtoService;
        }

        /// <summary>
        /// Exibe o menu principal e permite a seleção de opções.
        /// </summary>
        public void ExibirMenuPrincipal()
        {
            while (true)
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

                AguardarConfirmacao();
            }
        }

        /// <summary>
        /// Exibe o menu de opções relacionadas a pedidos e permite a seleção de opções.
        /// </summary>
        private void MenuPedido()
        {
            while (true)
            {
                Console.WriteLine("Opções do CRUD:");
                Console.WriteLine("1. Criar um novo pedido");
                Console.WriteLine("2. Consultar todos os pedidos");
                Console.WriteLine("3. Consultar um pedido existente");
                Console.WriteLine("4. Alterar um pedido existente");
                Console.WriteLine("5. Excluir um pedido existente");
                Console.WriteLine("9. Voltar ao MENU PRINCIPAL");
                Console.WriteLine("0. Sair");

                Console.Write("Selecione uma opção: ");
                string opcao = Console.ReadLine();

                Console.Clear();

                switch (opcao)
                {
                    case "1":
                        string respostaUsuario;
                        do
                        {
                            pedidoService.CriarPedido();

                            perguntaUsuario:
                            Console.WriteLine("Deseja criar novo pedido? (s/n)");
                            respostaUsuario = Console.ReadLine();

                            if (AppUtils.ValidacaorespostaUsuario(respostaUsuario))
                            {
                                continue;
                            }
                            else
                            {
                                AppUtils.MensagemRespostaInvalidaUsuario();
                                goto perguntaUsuario;
                            }

                        } while (respostaUsuario.Equals("s"));
                        break;
                    case "2":
                        pedidoService.ConsultarTodosPedidos();
                        break;
                    case "3":
                        pedidoService.ConsultarPedido();
                        break;
                    case "4":
                        pedidoService.AlterarPedido();
                        break;
                    case "5":
                        pedidoService.ExcluirPedido();
                        break;
                    case "9":
                        return;
                    case "0":
                        Console.WriteLine("Encerrando o programa...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }

                AguardarConfirmacao();
            }
        }

        /// <summary>
        /// Exibe o menu de opções relacionadas a produtos e permite a seleção de opções.
        /// </summary>
        private void MenuProduto()
        {
            while (true)
            {
                Console.WriteLine("Opções do CRUD:");
                Console.WriteLine("1. Criar um novo produto");
                Console.WriteLine("2. Consultar todos os produtos");
                Console.WriteLine("3. Consultar um produto existente");
                Console.WriteLine("4. Alterar um produto existente");
                Console.WriteLine("5. Excluir um produto existente");
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
                        produtoService.ConsultarTodosProdutos();
                        break;
                    case "3":
                        produtoService.ConsultarProduto();
                        break;
                    case "4":
                        produtoService.AlterarProduto();
                        break;
                    case "5":
                        produtoService.ExcluirProduto();
                        break;
                    case "9":
                        return;
                    case "0":
                        Console.WriteLine("Encerrando o programa...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }

                AguardarConfirmacao();
            }
        }

        /// <summary>
        /// Aguarda a confirmação do usuário antes de continuar.
        /// </summary>
        private void AguardarConfirmacao()
        {
            Console.WriteLine("Pressione Enter para continuar...");
            Console.ReadLine();

            Console.Clear();
        }
    }
}
