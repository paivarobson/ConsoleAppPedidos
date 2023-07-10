using ConsoleAppPedidos.Data;
using ConsoleAppPedidos.Data.Repositories;
using ConsoleAppPedidos.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppPedidos.Services
{
    /// <summary>
    /// Classe responsável por realizar operações relacionadas a pedidos.
    /// </summary>
    public class PedidoService
    {
        private readonly AppDbContexto dbContexto;
        private readonly PedidoRepository pedidoRepository;
        private readonly ItemDoPedidoRepository itemPedidoRepository;
        private readonly ProdutoRepository produtoRepository;

        /// <summary>
        /// Construtor da classe PedidoService.
        /// </summary>
        public PedidoService()
        {
            dbContexto = new AppDbContexto();
            pedidoRepository = new PedidoRepository(dbContexto);
            itemPedidoRepository = new ItemDoPedidoRepository(dbContexto);
            produtoRepository = new ProdutoRepository(dbContexto);
        }

        /// <summary>
        /// Cria um novo pedido.
        /// </summary>
        public void CriarPedido()
        {
            try
            {
                List<Pedido> listaPedidos = new List<Pedido>();
                List<ItemDoPedido> listaItensPedido = new List<ItemDoPedido>();

                string respostaUsuario;
                int produtoIdInseridoPedido;
                bool produtoCadastrado = false;

                Console.WriteLine("Opção de criação de pedido selecionada.");

                do
                {
                    Console.Clear();

                    Console.WriteLine("###################################");
                    Console.WriteLine("       Criando novo pedido...      ");
                    Console.WriteLine("###################################");

                    string identificador = GerarIdentificadorDoPedido();
                    Console.Write("Descrição: ");
                    string descricao = Console.ReadLine();

                    var novoPedido = new Pedido
                    {
                        Identificador = identificador,
                        Descricao = descricao
                    };

                    listaPedidos.Add(novoPedido);

                    do
                    {
                        Console.Write("Código do Produto: ");
                        int produtoId = int.Parse(Console.ReadLine());
                        bool itemJaAdicionadoAoPedidoExistente = false;

                        produtoCadastrado = produtoRepository.ConsultarProdutoExiste(produtoId);

                        if (produtoCadastrado)
                        {
                            if (listaItensPedido.Count() > 0)
                            {
                                foreach (var item in listaItensPedido)
                                {
                                    if (item.ProdutoID == produtoId)
                                    {
                                        Console.WriteLine("Produto já adicionado ao pedido.");

                                        itemJaAdicionadoAoPedidoExistente = true;
                                        break;
                                    }
                                }
                            }

                            if (!itemJaAdicionadoAoPedidoExistente)
                            {
                                Console.Write("Quantidade: ");
                                int quantidade = int.Parse(Console.ReadLine());

                                while (quantidade < 1)
                                {
                                    Console.Write("Quantidade deve ser maior que 0. Tente novamente: ");
                                    quantidade = int.Parse(Console.ReadLine());
                                }

                                Console.Write("Valor Unitário: ");
                                double valorUnitario = double.Parse(Console.ReadLine());

                                while (valorUnitario < 0.00)
                                {
                                    Console.Write("Valor Unitário deve ser maior ou igual a R$ 0. Tente novamente: ");
                                    valorUnitario = double.Parse(Console.ReadLine());
                                }

                                var novoItem = new ItemDoPedido
                                {
                                    ProdutoID = produtoId,
                                    Quantidade = quantidade,
                                    Valor = valorUnitario
                                };

                                listaItensPedido.Add(novoItem);
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("Produto não encontrado.");
                        }

                        perguntaUsuario:
                        Console.Write("Deseja adicionar novo item? (s/n):");
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

                } while (!produtoCadastrado);

                foreach (var pedido in listaPedidos)
                {
                    pedidoRepository.CriarPedido(pedido);

                    foreach (var itemDoProduto in listaItensPedido)
                    {
                        itemPedidoRepository.AdicionarItemAoPedido(pedido.ID, itemDoProduto);
                    }

                    pedido.ValorTotal = CalcularValorTotal(pedido.ID);
                    pedidoRepository.SalvarPedido();
                }

                Console.WriteLine("###################################");
                Console.WriteLine("     Pedido criado com sucesso!    ");
                Console.WriteLine("###################################");

                Console.Clear();
                ImprimirPedido(listaPedidos);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao criar o pedido: " + ex.Message);
            }
        }

        /// <summary>
        /// Consulta e imprime todos os pedidos cadastrados.
        /// </summary>
        /// <exception cref="InvalidOperationException">Exceção lançada quando ocorre um erro ao consultar os pedidos.</exception>
        /// <exception cref="Exception">Exceção genérica lançada quando ocorre um erro ao consultar ou imprimir os pedidos.</exception>
        public void ConsultarTodosPedidos()
        {
            try
            {
                var pedidos = pedidoRepository.ConsultarTodosPedidos();

                ImprimirPedido(pedidos.ToList());
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao consultar todos os pedidos: " + ex.Message);
            }
        }

        /// <summary>
        /// Consulta um pedido pelo seu ID.
        /// </summary>
        /// <param name="pedidoId">ID do pedido a ser consultado.</param>
        public void ConsultarPedido(int pedidoId = 0)
        {
            try
            {
                string respostaUsuario;

                Console.WriteLine("Opção de consulta de pedido selecionada.");

                do
                {
                    if (pedidoId == 0)
                    {
                        Console.WriteLine("Digite o código do pedido:");

                        pedidoId = int.Parse(Console.ReadLine());
                    }

                    var pedido = pedidoRepository.ConsultarPedido(pedidoId);

                    var listaPedido = new List<Pedido>
                    {
                        pedido
                    };

                    if (pedido != null)
                    {
                        ImprimirPedido(listaPedido);

                        pedidoId = 0;
                    }
                    else
                    {
                        Console.WriteLine($"Pedido {pedidoId} não localizado.");
                    }

                    perguntaUsuario:
                    Console.Write("Deseja consultar novo pedido? (s/n):");
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
                Console.WriteLine($"Ocorreu um erro ao consultar o pedido: {ex.Message}");
            }
        }

        /// <summary>
        /// Altera um pedido existente.
        /// </summary>
        public void AlterarPedido()
        {
        	try
        	{
                List<Pedido> listaPedidosAlterados = new List<Pedido>();
                string respostaUsuario;

                Console.Clear();

                Console.WriteLine("Opção de atualização de pedido selecionada.");

                do
                {
                    var todosPedidosEncontrados = pedidoRepository.ConsultarTodosPedidos();

                    ImprimirPedido(todosPedidosEncontrados.ToList());

                    Console.WriteLine("Digite o código do pedido:");
                    int pedidoId = int.Parse(Console.ReadLine());

                    var pedidoEncontrado = pedidoRepository.ConsultarPedido(pedidoId);

                    if (pedidoEncontrado != null)
                    {
                        Console.WriteLine("Digite a nova descrição:");
                        string descricao = Console.ReadLine();

                        var pedidoAlterado = new Pedido
                        {
                            ID = pedidoEncontrado.ID,
                            Identificador = pedidoEncontrado.Identificador,
                            Descricao = descricao,
                            ValorTotal = pedidoEncontrado.ValorTotal
                        };

                        pedidoRepository.AlterarPedido(pedidoAlterado);

                        if (!listaPedidosAlterados.Any(p => p.ID == pedidoAlterado.ID))
                        {
                            listaPedidosAlterados.Add(pedidoRepository.ConsultarPedido(pedidoAlterado.ID));
                        }

                        Console.Clear();
                        ImprimirPedido(listaPedidosAlterados);
                        Console.WriteLine("Pedido(s) alterado(s) com sucesso.");
                    }
                    else
                    {
                        Console.WriteLine($"Pedido {pedidoId} não encontrado.");
                    }

                    perguntaUsuario:
                    Console.Write("Deseja alterar outro pedido? (s/n):");
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

                Console.Clear();
                Console.WriteLine("Pedido(s) alterado(s):");
                ImprimirPedido(listaPedidosAlterados);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
	        catch (Exception ex)
	        {
	            Console.WriteLine("Ocorreu um erro ao alterar o pedido: " + ex.Message);
	        }
	    }

        /// <summary>
        /// Exclui um pedido existente e seus itens associados.
        /// </summary>
        public void ExcluirPedido()
	    {
	        try
	        {
                Console.WriteLine("Opção de exclusão de pedido selecionada.");

                string respostaUsuario;

                do
                {
                    Console.Clear();
                    var listaPedidos = pedidoRepository.ConsultarTodosPedidos().ToList();

                    ImprimirPedido(listaPedidos);

                    Console.WriteLine("Digite o código do pedido:");
                    int pedidoId = int.Parse(Console.ReadLine());

                    var pedidoEncontrado = pedidoRepository.ConsultarPedido(pedidoId);

                    if (pedidoEncontrado != null)
                    {
                        var itensDoPedido = itemPedidoRepository.ConsultarItensDoPedido(pedidoId);

                        pedidoRepository.ExcluirPedido(pedidoEncontrado);

                        foreach (var item in itensDoPedido)
                        {
                            itemPedidoRepository.ExcluirItemDoPedido(item);
                        }

                        Console.WriteLine($"Pedido {pedidoId} excluído com sucesso.");
                    }
                    else
                    {
                        Console.WriteLine($"Pedido {pedidoId} não localizado.");
                    }

                    perguntaUsuario:
                    Console.Write("Deseja excluir outro pedido? (s/n):");
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

                } while (respostaUsuario.Equals("s")) ;

            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
	        {
	            Console.WriteLine("Ocorreu um erro ao excluir o pedido: " + ex.Message);
	        }
	    }

        /// <summary>
        /// Gera um identificador único para um novo pedido no formato "P_[letra, seguida de 3 números]_C".
        /// </summary>
        /// <returns>O identificador gerado para o novo pedido.</returns>
        /// <exception cref="Exception">Exceção lançada quando ocorre um erro ao calcular o valor total do pedido no banco de dados.</exception>
        public string GerarIdentificadorDoPedido()
        {
            try
            {
                string proximoIdentificador;

                // Obtém o último pedido registrado no banco de dados
                var ultimoPedido = pedidoRepository.ConsultarUltimoPedido();

                int proximoNumero = 0;
                char proximaLetra = 'A';

                if (ultimoPedido != null)
                {
                    // Obtém o identificador do último pedido
                    string ultimoIdentificador = ultimoPedido.Identificador;
                    char ultimaLetra = ultimoIdentificador[2];

                    // Extrai o número do identificador do último pedido
                    int ultimoNumero = int.Parse(ultimoIdentificador.Substring(3, 3));

                    // Verifica se o número do último identificador é igual a 999
                    // Se for, avança para a próxima letra; caso contrário, incrementa o número atual
                    if (ultimoNumero == 999)
                    {
                        proximaLetra = (char)(ultimaLetra + 1);
                    }
                    else
                    {
                        proximaLetra = ultimaLetra;
                        proximoNumero = ultimoNumero + 1;
                    }
                }

                // Gera o identificador para o novo pedido com base na próxima letra e número disponíveis
                proximoIdentificador = $"P_{proximaLetra}{proximoNumero.ToString("D3")}_C";

                return proximoIdentificador;
            }
            catch (InvalidOperationException ex)
            {
                return "P_A000_C";
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar o identificador do pedido.", ex);
            }
        }

        /// <summary>
        /// Calcula o valor total de um pedido, somando os valores unitários dos itens do pedido.
        /// </summary>
        /// <param name="pedidoId">O ID do pedido.</param>
        /// <returns>O valor total do pedido.</returns>
        /// <exception cref="Exception">Exceção lançada quando ocorre um erro ao calcular o valor total do pedido no banco de dados.</exception>
		public double CalcularValorTotal(int pedidoId)
        {
            try
            {
                var pedido = pedidoRepository.ConsultarPedido(pedidoId);

                if (pedido != null)
                {
                    double valorTotal = dbContexto.ItensDePedido
                        .Where(i => i.PedidoID == pedidoId)
                        .Sum(i => i.Quantidade * i.Valor);

                    return valorTotal;
                }

                return 0.0;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao calcular o valor total do pedido no banco de dados.", ex);
            }
        }

        /// <summary>
        /// Imprime os detalhes de um ou mais pedidos, incluindo seus itens.
        /// </summary>
        /// <param name="pedidos">A lista de pedidos a serem impressos.</param>
        /// <exception cref="InvalidOperationException">Exceção lançada quando ocorre um erro ao consultar o pedido ou seus itens, ou o pedido não é encontrado.</exception>
        /// <exception cref="Exception">Exceção genérica lançada quando ocorre um erro ao imprimir o pedido.</exception>
        public void ImprimirPedido(List<Pedido> pedidos)
        {
            try
            {
                foreach (var pedido in pedidos)
                {
                    var itensDoPedido = itemPedidoRepository.ConsultarItensDoPedido(pedido.ID).ToList();

                    int contador = 1;

                    Console.WriteLine("###################################");

                    Console.Write(
                        $"Cód. Pedido: {pedido.ID} | " +
                        $"Identificador: {pedido.Identificador} | " +
                        $"Descrição: {pedido.Descricao} | " +
                        $"Valor total: {pedido.ValorTotal}\n");

                    foreach (var item in itensDoPedido)
                    {
                        var produto = produtoRepository.ConsultarProduto(item.ProdutoID);

                        Console.Write($"    Item : 00{contador}\n");
                        Console.Write($"### Cod. Produto: {produto.ID}\n");
                        Console.Write($"### Produto: {produto.Nome}\n");
                        Console.Write($"### Categoria: {AppUtils.CarregarCategoriaProduto(produto.Categoria)}\n");
                        Console.Write($"### Qtd: {item.Quantidade}\n");
                        Console.Write($"### Vlr. Unit.: {item.Valor}\n");

                        contador++;
                    }

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
