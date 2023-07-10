using ConsoleAppPedidos.Data;
using ConsoleAppPedidos.Data.Repositories;
using ConsoleAppPedidos.Models;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                Console.WriteLine("Opção de criação de produto selecionada.");

                Console.WriteLine("###################################");
                Console.WriteLine("       Cadastrando novo produto...      ");
                Console.WriteLine("###################################");

                string respostaUsuario;
                List<Produto> listaProdutos = new List<Produto>();

                do
                {
                    Console.Write("Descrição do Produto: ");
                    string nome = Console.ReadLine();
                    Console.Write("Categoria (0 - Perecível ou 1 - Não perecível): ");
                    int categoria = int.Parse(Console.ReadLine());

                    var novoProduto = new Produto
                    {
                        Nome = nome,
                        Categoria = categoria
                    };

                    listaProdutos.Add(novoProduto);

                    perguntaUsuario:
                    Console.Write("Deseja adicionar novo produto? (s/n): ");
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

                foreach (var item in listaProdutos)
                {
                    produtoRepository.CriarProduto(item);
                }

                Console.Clear();

                Console.WriteLine("###################################");
                Console.WriteLine("  Produto(s) criado(s) com sucesso! ");
                Console.WriteLine("###################################");

                ImprimirProduto(listaProdutos);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao criar o produto: " + ex.Message);
            }
        }

        internal void ConsultarTodosProdutos()
        {
            try
            {
                var produtos = produtoRepository.ConsultarTodosProdutos();

                ImprimirProduto(produtos.ToList());
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao consultar todos os produtos: " + ex.Message);
            }
        }

        /// <summary>
        /// Consulta um produto pelo seu ID.
        /// </summary>
        /// <param name="produtoId">ID do produto a ser consultado (opcional).</param>
        public void ConsultarProduto(int produtoId = 0)
        {
            try
            {
                string respostaUsuario;
                Console.WriteLine("Opção de consulta de produto selecionada.");

                do
                {
                    Console.Clear();

                    if (produtoId == 0)
                    {
                        Console.WriteLine("Digite o código do produto:");
                        produtoId = int.Parse(Console.ReadLine());
                    }

                    var produtoEncontrado = produtoRepository.ConsultarProduto(produtoId);

                    if (produtoEncontrado != null)
                    {
                        List<Produto> listaProdutos = new List<Produto>
                        {
                            produtoEncontrado
                        };

                        ImprimirProduto(listaProdutos);
                    }
                    else
                    {
                        Console.WriteLine($"Pedido {produtoId} não localizado.");
                    }

                    produtoId = 0;

                    perguntaUsuario:
                    Console.Write("Deseja consultar novo produto? (s/n): ");
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao consultar o produto: " + ex.Message);
            }
        }

        /// <summary>
        /// Exclui um produto existente.
        /// </summary>
        public void ExcluirProduto()
        {
            try
            {
                Console.WriteLine("Opção de exclusão do produto selecionado.");

                string respostaUsuario;

                do
                {
                    Console.Clear();
                    var listaProdutos = produtoRepository.ConsultarTodosProdutos().ToList();

                    ImprimirProduto(listaProdutos);

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
                            Console.WriteLine($"Produto {produtoId} não encontrado.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Produto não pode ser excluído pois está associado a um pedido.");
                    }

                    perguntaUsuario:
                    Console.WriteLine("Deseja excluir outro produto? (s/n)");
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao excluir o produto: " + ex.Message);
            }
        }

        /// <summary>
        /// Altera um produto existente.
        /// </summary>
        public void AlterarProduto()
        {
            try
            {
                Console.WriteLine("Opção de atualização do produto selecionado.");

                List<Produto> listaProdutosAlterados = new List<Produto>();
                string respostaUsuario;

                do
                {
                    Console.Clear();

                    var todosProdutos = produtoRepository.ConsultarTodosProdutos();

                    if (todosProdutos.Count() > 0)
                    {
                        ImprimirProduto(todosProdutos.ToList());
                    }
                    else
                    {
                        Console.WriteLine("Ainda não existe produto cadastrado no banco de dados");
                    }

                    Console.WriteLine("Digite o código do produto:");
                    int produtoId = int.Parse(Console.ReadLine());

                    bool produtoAssociadoAoPedido = itemPedidoRepository.ProdutoAssociadoPedido(produtoId);

                    if (!produtoAssociadoAoPedido)
                    {
                        var produtoEncontrado = produtoRepository.ConsultarProduto(produtoId);

                        if (produtoEncontrado != null)
                        {
                            List<Produto> listaProduto = new List<Produto>
                            {
                                produtoEncontrado
                            };

                            Console.Clear();
                            ImprimirProduto(listaProduto);

                            Console.WriteLine("Digite a nova descrição:");
                            string descricao = Console.ReadLine();
                            Console.WriteLine("Digite a nova categoria (0 - Perecível ou 1 - Não perecível):");
                            int categoria = int.Parse(Console.ReadLine());

                            var produtoAlterado = new Produto
                            {
                                ID = produtoEncontrado.ID,
                                Nome = descricao,
                                Categoria = categoria
                            };

                            produtoRepository.AlterarProduto(produtoAlterado);

                            if (!listaProdutosAlterados.Any(p => p.ID == produtoAlterado.ID))
                            {
                                listaProdutosAlterados.Add(produtoRepository.ConsultarProduto(produtoAlterado.ID));
                            }

                            Console.Clear();
                            ImprimirProduto(listaProdutosAlterados);
                            Console.WriteLine("Produto(s) alterado(s) com sucesso.");
                        }
                        else
                        {
                            Console.WriteLine($"Produto {produtoId} não encontrado.");
                        }                        
                    }
                    else
                    {
                        Console.WriteLine("Alteração não permitida. Categoria de produto presente em pedido.");
                    }

                    perguntaUsuario:
                    Console.WriteLine("Deseja alterar novo produto? (s/n)");
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
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao alterar o produto: " + ex.Message);
            }
        }

        /// <summary>
        /// Imprime os detalhes de um produto.
        /// </summary>
        /// <param name="produtoId">O ID do produto a ser impresso.</param>
        /// <exception cref="InvalidOperationException">Exceção lançada quando ocorre um erro ao consultar o produto ou o produto não é encontrado.</exception>
        /// <exception cref="Exception">Exceção genérica lançada quando ocorre um erro ao imprimir o produto.</exception>
        public void ImprimirProduto(List<Produto> listaProdutos)
        {
            try
            {
                foreach (var item in listaProdutos)
                {
                    Console.WriteLine("###################################");

                    Console.Write(
                        $"Cód. Produto: {item.ID} | " +
                        $"Descrição: {item.Nome} | " +
                        $"Categoria: {AppUtils.CarregarCategoriaProduto(item.Categoria)}\n");

                    Console.WriteLine("###################################");
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
