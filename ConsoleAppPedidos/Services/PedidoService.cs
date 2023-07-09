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
                Console.WriteLine("Opção de criação de pedido selecionada.");

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

                pedidoRepository.CriarPedido(novoPedido);

                string adicionarNovoItem;
                ItemDoPedido novoItem;

                do
                {
                    Console.Write("Código do Produto: ");
                    int produtoId = int.Parse(Console.ReadLine());
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

                    novoItem = new ItemDoPedido
                    {
                        ProdutoID = produtoId,
                        Quantidade = quantidade,
                        Valor = valorUnitario
                    };

                    bool itemExistenteNoPedido = itemPedidoRepository.ConsultarItensDoPedido(novoPedido.ID).Any(i => i.ProdutoID == produtoId);

                    var produtoExiste = produtoRepository.ConsultarProduto(produtoId);

                    if (!itemExistenteNoPedido)
                    {
                        if (produtoExiste != null)
                            itemPedidoRepository.AdicionarItemAoPedido(novoPedido.ID, novoItem);
                        else
                            Console.WriteLine("Produto não cadastrado.");
                    }
                    else
                    {
                        Console.WriteLine("Produto já presente no item de pedido.");
                    }

                    Console.Write("Deseja adicionar novo item? (y/n):");
                    adicionarNovoItem = Console.ReadLine();

                } while (adicionarNovoItem.Equals("y"));

                novoPedido.ValorTotal = CalcularValorTotal(novoPedido.ID);

                dbContexto.SaveChanges();

                Console.WriteLine("###################################");
                Console.WriteLine("     Pedido criado com sucesso!    ");
                Console.WriteLine("###################################");

                ImprimirPedido(novoPedido.ID);
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

        public void ConsultarTodosPedidos()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Consulta um pedido pelo seu ID.
        /// </summary>
        /// <param name="pedidoId">ID do pedido a ser consultado.</param>
        public void ConsultarPedido(int pedidoId = 0)
        {
            try
            {
                string consultarNovoPedido;

                do
                {
                    if (pedidoId == 0)
                    {
                        Console.WriteLine("Opção de consulta de pedido selecionada.");
                        Console.WriteLine("Digite o código do pedido:");

                        pedidoId = int.Parse(Console.ReadLine());
                    }

                    ImprimirPedido(pedidoId);

                    pedidoId = 0;

                    Console.Write("Deseja consultar novo pedido? (y/n):");
                    consultarNovoPedido = Console.ReadLine();

                } while (consultarNovoPedido.Equals("y"));
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
	            Console.WriteLine("Opção de atualização de pedido selecionada.");

	            Console.WriteLine("Digite o código do pedido:");
	            int pedidoId = int.Parse(Console.ReadLine());

	            Console.Clear();
	            pedidoRepository.ConsultarPedido(pedidoId);

	            Console.WriteLine("Digite o novo Identificador:");
	            string identificador = Console.ReadLine();
	            Console.WriteLine("Digite a nova descrição:");
	            string descricao = Console.ReadLine();
	            Console.WriteLine("Digite o valor total:");
	            double valorTotal = int.Parse(Console.ReadLine());

	            var pedidoEncontrado = pedidoRepository.ConsultarPedido(pedidoId);

	            var pedido = new Pedido
	            {
	                ID = pedidoEncontrado.ID,
	                Identificador = identificador,
	                Descricao = descricao,
	                ValorTotal = valorTotal
	            };

                pedidoRepository.AlterarPedido(pedido);

                ImprimirPedido(pedido.ID);

	            Console.WriteLine("Pedido alterado com sucesso.");
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
	            Console.WriteLine("Digite o código do pedido:");
	            int pedidoId = int.Parse(Console.ReadLine());

	            var pedidoEncontrado = pedidoRepository.ConsultarPedido(pedidoId);

	            if (pedidoEncontrado != null)
	            {
                    ImprimirPedido(pedidoEncontrado.ID);

                    var itensDoPedido = itemPedidoRepository.ConsultarItensDoPedido(pedidoId);

	                pedidoRepository.ExcluirPedido(pedidoEncontrado);

	                foreach (var item in itensDoPedido)
	                {
	                    itemPedidoRepository.ExcluirItemDoPedido(item);
	                }

	                Console.WriteLine("Pedido excluído com sucesso.");
	            }
	            else
	            {
	                Console.WriteLine("Pedido não localizado.");
	            }
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
                string proximoIdentificador = $"P_{proximaLetra}{proximoNumero.ToString("D3")}_C";

                return proximoIdentificador;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
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
        /// Imprime os detalhes de um pedido, incluindo seus itens.
        /// </summary>
        /// <param name="pedidoId">O ID do pedido a ser impresso.</param>
        /// <exception cref="InvalidOperationException">Exceção lançada quando ocorre um erro ao consultar o pedido ou seus itens, ou o pedido não é encontrado.</exception>
        /// <exception cref="Exception">Exceção genérica lançada quando ocorre um erro ao imprimir o pedido.</exception>
        public void ImprimirPedido(int pedidoId)
        {
            try
            {
                var pedido = pedidoRepository.ConsultarPedido(pedidoId);
                var itensDoPedido = itemPedidoRepository.ConsultarItensDoPedido(pedidoId);
                var produtos = produtoRepository.ConsultarTodosProdutos().AsQueryable();

                int contador = 1;

                Console.WriteLine("###################################");

                Console.Write(
                    $"Cód. Pedido: {pedido.ID} | " +
                    $"Identificador: {pedido.Identificador} | " +
                    $"Descrição: {pedido.Descricao} | " +
                    $"Valor total: {pedido.ValorTotal}\n");

                foreach (var item in itensDoPedido)
                {
                    var produto = produtos.FirstOrDefault(p => p.ID == item.ProdutoID);

                    Console.Write($"    Item : 00{contador}\n");
                    Console.Write($"### Cod. Produto: {item.ProdutoID}\n");
                    Console.Write($"### Produto: {item.Produto.Nome}\n");
                    Console.Write($"### Categoria: {AppUtils.CarregarCategoriaProduto(produto.Categoria)}\n");
                    Console.Write($"### Qtd: {item.Quantidade}\n");
                    Console.Write($"### Vlr. Unit.: {item.Valor}\n");

                    contador++;
                }

                Console.WriteLine("###################################");
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
