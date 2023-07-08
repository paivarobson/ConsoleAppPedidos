using ConsoleAppPedidos.Data;
using ConsoleAppPedidos.Data.Repositories;
using ConsoleAppPedidos.Models;

namespace ConsoleAppPedidos.Services
{
    /// <summary>
    /// Classe responsável por realizar operações relacionadas a produtos.
    /// </summary>
    public class ProdutoService
    {
        private readonly AppDbContexto dbContexto;
        private readonly ProdutoRepository produtoRepository;
        private readonly ItemDoPedidoRepository itemPedidoRepository;

        /// <summary>
        /// Construtor da classe ProdutoService.
        /// </summary>
        public ProdutoService()
        {
            dbContexto = new AppDbContexto();
            produtoRepository = new ProdutoRepository(dbContexto);
            itemPedidoRepository = new ItemDoPedidoRepository(dbContexto);
        }

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        public void CriarProduto()
        {
            Console.WriteLine("Opção de criação de produto selecionada.");

            Console.WriteLine("###################################");
            Console.WriteLine("       Cadastrando novo produto...      ");
            Console.WriteLine("###################################");

            string adicionarNovoProduto;
            var novoProduto = new Produto();

            do
            {
                Console.Write("Descrição do Produto: ");
                string nome = Console.ReadLine();
                Console.Write("Categoria (0 - Perecível) ou 1 - Não perecível): ");
                int categoria = int.Parse(Console.ReadLine());

                novoProduto = new Produto
                {
                    Nome = nome,
                    Categoria = categoria
                };

                produtoRepository.CriarProduto(novoProduto);

                Console.Write("Deseja adicionar novo produto? (y/n): ");
                adicionarNovoProduto = Console.ReadLine();

            } while (adicionarNovoProduto.Equals("y"));

            Console.WriteLine("###################################");
            Console.WriteLine("     Produto(s) criado(s) com sucesso!    ");
            Console.WriteLine("###################################");

            ConsultarProduto(novoProduto.ID);
        }

        /// <summary>
        /// Consulta um produto pelo seu ID.
        /// </summary>
        /// <param name="produtoId">ID do produto a ser consultado.</param>
        public void ConsultarProduto(int produtoId = 0)
        {
            string consultarNovoProduto;

            do
            {
                if (produtoId == 0)
                {
                    Console.WriteLine("Opção de consulta de produto selecionada.");
                    Console.WriteLine("Digite o código do produto:");

                    produtoId = int.Parse(Console.ReadLine());
                }

                var produto = produtoRepository.ConsultarProduto(produtoId);

                if (produto != null)
                {
                    Console.WriteLine("###################################");

                    Console.Write(
                        $"Cód. Produto: {produto.ID} | " +
                        $"Descrição: {produto.Nome} | " +
                        $"Categoria: {AppUtils.CarregarCategoriaProduto(produtoId)}\n");

                    Console.WriteLine("###################################");

                }
                else
                {
                    Console.WriteLine("Produto não localizado.");
                }

                Console.Write("Deseja consultar novo produto? (y/n):");
                consultarNovoProduto = Console.ReadLine();

                produtoId = 0;

            } while (consultarNovoProduto.Equals("y"));
        }

        /// <summary>
        /// Exclui um produto existente.
        /// </summary>
        public void ExcluirProduto()
        {
            Console.WriteLine("Opção de exclusão do produto selecionado.");

            string excluirNovoProduto = "n";

            do
            {
                Console.WriteLine("Digite o código do produto:");
                int produtoId = int.Parse(Console.ReadLine());

                bool produtoAssociadoAoPedido = itemPedidoRepository.ProdutoAssociadoPedido(produtoId);

                if (!produtoAssociadoAoPedido)
                {
                    Console.Clear();
                    var produtoEncontrado = produtoRepository.ConsultarProduto(produtoId);

                    if (produtoEncontrado != null)
                    {
                        produtoRepository.ExcluirProduto(produtoEncontrado);

                        Console.WriteLine("Produto excluído com sucesso.");
                    }
                    else
                    {
                        Console.WriteLine("Produto não encontrado.");
                    }
                }
                else
                {
                    Console.WriteLine("Produto não pode ser excluído pois está associado a um pedido.");
                }

                Console.WriteLine("Deseja excluir outro produto? (y/n)");
                excluirNovoProduto = Console.ReadLine();

            } while (excluirNovoProduto.Equals("y"));
        }

        /// <summary>
        /// Altera um produto existente.
        /// </summary>
        public void AlterarProduto()
        {
            Console.WriteLine("Opção de atualização do produto selecionado.");

            string alterarNovoProduto = "n";

            do
            {
                Console.WriteLine("Digite o código do produto:");
                int produtoId = int.Parse(Console.ReadLine());

                bool produtoAssociadoAoPedido = itemPedidoRepository.ProdutoAssociadoPedido(produtoId);

                if (!produtoAssociadoAoPedido)
                {
                    Console.Clear();

                    var produtoEncontrado = produtoRepository.ConsultarProduto(produtoId);

                    if (produtoEncontrado != null)
                    {
                        ConsultarProduto(produtoId);

                        Console.WriteLine("Digite a nova descrição:");
                        string descricao = Console.ReadLine();
                        Console.WriteLine("Digite a nova categoria (0 - Perecível) ou 1 - Não perecível):");
                        int categoria = int.Parse(Console.ReadLine());

                        produtoEncontrado = new Produto
                        {
                            ID = produtoEncontrado.ID,
                            Nome = descricao,
                            Categoria = categoria
                        };

                        bool alteradoComSucesso = produtoRepository.AlterarProduto(produtoEncontrado);

                        if (alteradoComSucesso)
                        {
                            Console.WriteLine("Produto alterado com sucesso.");
                        }
                        else
                        {
                            Console.WriteLine("Falha na alteração do produto.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Produto não localizado.");
                    }
                }
                else
                {
                    Console.WriteLine("Alteração não permitida. Categoria de produto presente em pedido.");
                }

                Console.WriteLine("Deseja alterar novo produto? (y/n)");
                alterarNovoProduto = Console.ReadLine();

            } while (alterarNovoProduto.Equals("y"));
        }
    }
}
