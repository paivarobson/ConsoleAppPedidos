using ConsoleAppPedidos.Data;
using ConsoleAppPedidos.Data.Repositories;
using ConsoleAppPedidos.Models;

namespace ConsoleAppPedidos.Services
{
    /// <summary>
    /// Classe responsável por realizar operações relacionadas a pedidos.
    /// </summary>
    public class PedidoService
    {
        /// <summary>
        /// Cria um novo pedido.
        /// </summary>
        public void CriarPedido()
        {
            Console.WriteLine("Opção de criação de pedido selecionada.");

            using (var dbContexto = new DBContexto())
            {
                var pedidoRepository = new PedidoRepository(dbContexto);
                var itemPedidoRepository = new ItemDoPedidoRepository(dbContexto);

                Console.WriteLine("###################################");
                Console.WriteLine("       Criando novo pedido...      ");
                Console.WriteLine("###################################");

                // Gera o identificador do pedido e define a descrição padrão
                string identificador = pedidoRepository.GerarIdentificadorDoPedido();
                Console.Write("Descrição: ");
                string descricao = Console.ReadLine();

                // Cria uma nova instância do pedido
                var novoPedido = new Pedido
                {
                    Identificador = identificador,
                    Descricao = descricao
                };

                // Cria o pedido no repositório
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
                        quantidade = int.Parse(Console.ReadLine());
                    }

                    // Cria um novo item do pedido
                    novoItem = new ItemDoPedido
                    {
                        ProdutoID = produtoId,
                        Quantidade = quantidade,
                        Valor = valorUnitario
                    };

                    bool itemExistenteNoPedido = itemPedidoRepository.ConsultarItensDoPedido(novoPedido.ID).Any(i => i.ProdutoID == produtoId);

                    if (!itemExistenteNoPedido)
                    {
                        // Adiciona o item ao pedido no repositório
                        itemPedidoRepository.AdicionarItemAoPedido(novoPedido.ID, novoItem);
                    }
                    else
                    {
                        Console.WriteLine("Produto já presente no item de pedido.");
                    }

                    Console.Write("Deseja adicionar novo item? (y/n):");
                    adicionarNovoItem = Console.ReadLine();

                } while (adicionarNovoItem.Equals("y"));

                // Calcula o valor total do pedido
                novoPedido.ValorTotal = pedidoRepository.CalcularValorTotal(novoPedido.ID);

                // Salva as alterações no banco de dados
                dbContexto.SaveChanges();

                Console.WriteLine("###################################");
                Console.WriteLine("     Pedido criado com sucesso!    ");
                Console.WriteLine("###################################");

                ConsultarPedido(novoPedido.ID);
            }
        }

        /// <summary>
        /// Consulta um pedido pelo seu ID.
        /// </summary>
        /// <param name="pedidoId">ID do pedido a ser consultado.</param>
        public void ConsultarPedido(int pedidoId = 0)
        {
            using (var dbContexto = new DBContexto())
            {
                var pedidoRepository = new PedidoRepository(dbContexto);
                var itensDoPedidoRepository = new ItemDoPedidoRepository(dbContexto);
                var produtoRepository = new ProdutoRepository(dbContexto);

                string consultarNovoPedido;

                do
                {
                    if (pedidoId == 0)
                    {
                        Console.WriteLine("Opção de consulta de pedido selecionada.");
                        Console.WriteLine("Digite o código do pedido:");

                        pedidoId = int.Parse(Console.ReadLine());
                    }

                    var pedido = pedidoRepository.ConsultarPedido(pedidoId);
                    var itensDoPedido = itensDoPedidoRepository.ConsultarItensDoPedido(pedidoId);
                    var produtos = produtoRepository.ConsultarProdutos().ToList();

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
                        Console.Write($"### Categoria: {HelpTools.CarregarCategoriaProduto(produto.Categoria)}\n");
                        Console.Write($"### Qtd: {item.Quantidade}\n");
                        Console.Write($"### Vlr. Unit.: {item.Valor}\n");

                        contador++;
                    }

                    pedidoId = 0;

                    Console.WriteLine("###################################");

                    Console.Write("Deseja consultar novo pedido? (y/n):");
                    consultarNovoPedido = Console.ReadLine();

                } while (consultarNovoPedido.Equals("y"));
            }
        }

        /// <summary>
        /// Altera um pedido existente.
        /// </summary>
        public void AlterarPedido()
        {
            using (var dbContexto = new DBContexto())
            {
                var pedidoRepository = new PedidoRepository(dbContexto);
                var itemPedidoRepository = new ItemDoPedidoRepository(dbContexto);

                Console.WriteLine("Opção de atualização de pedido selecionada.");

                Console.WriteLine("Digite o código do pedido:");
                int pedidoId = int.Parse(Console.ReadLine());

                Console.Clear();
                ConsultarPedido(pedidoId);

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

                Console.WriteLine("Pedido alterado com sucesso.");
            }
        }

        /// <summary>
        /// Exclui um pedido existente e seus itens associados.
        /// </summary>
        public void ExcluirPedido()
        {
            using (var dbContexto = new DBContexto())
            {
                var pedidoRepository = new PedidoRepository(dbContexto);
                var itemPedidoRepository = new ItemDoPedidoRepository(dbContexto);

                Console.WriteLine("Opção de exclusão de pedido selecionada.");
                Console.WriteLine("Digite o código do pedido:");
                int pedidoId = int.Parse(Console.ReadLine());

                var pedidoEncontrado = pedidoRepository.ConsultarPedido(pedidoId);

                // Exclui os itens associados ao pedido
                var itensDoPedido = itemPedidoRepository.ConsultarItensDoPedido(pedidoId);
                foreach (var item in itensDoPedido)
                {
                    itemPedidoRepository.ExcluirItemDoPedido(item);
                }

                // Exclui o pedido
                pedidoRepository.ExcluirPedido(pedidoEncontrado);

                Console.WriteLine("Pedido excluído com sucesso.");
            }
        }
    }
}
