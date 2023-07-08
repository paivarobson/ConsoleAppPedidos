using ConsoleAppPedidos.Data;
using ConsoleAppPedidos.Data.Repositories;
using ConsoleAppPedidos.Models;

namespace ConsoleAppPedidos.Services
{
    public class PedidoService
    {
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
                string identificador = pedidoRepository.GerarIdentificadorDoPedido();
                string descricao = "Descrição padrão";

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
                    Console.Write("Valor Unitário: ");
                    double valorUnitario = double.Parse(Console.ReadLine());

                    novoItem = new ItemDoPedido
                    {
                        ProdutoID = produtoId,
                        Quantidade = quantidade,
                        Valor = valorUnitario
                    };

                    itemPedidoRepository.AdicionarItemAoPedido(novoPedido.ID, novoItem);

                    Console.Write("Deseja adicionar novo item? (y/n):");
                    adicionarNovoItem = Console.ReadLine();

                } while (adicionarNovoItem.Equals("y"));

                novoPedido.ValorTotal = pedidoRepository.CalcularValorTotal(novoPedido.ID);

                dbContexto.SaveChanges();

                Console.WriteLine("###################################");
                Console.WriteLine("     Pedido criado com sucesso!    ");
                Console.WriteLine("###################################");

                ConsultarPedido(novoPedido.ID);
            }
        }

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
                        Console.Write($"### Categoria: {CarregarCategoriaProduto(produto.Categoria)}\n");
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

            string CarregarCategoriaProduto(int categoriaId)
            {
                if (categoriaId == 0)
                    return "Perecível";
                else
                    return "Não perecível";
            }
        }

        public void AlterarPedido()
        {
            using (var dbContexto = new DBContexto())
            {
                PedidoRepository pedidoRepository = new PedidoRepository(dbContexto);
                ItemDoPedidoRepository itemPedidoRepository = new ItemDoPedidoRepository(dbContexto);

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
            }
        }

        public void ExcluirPedido()
        {
            using (var dbContexto = new DBContexto())
            {
                PedidoRepository pedidoRepository = new PedidoRepository(dbContexto);
                ItemDoPedidoRepository itemPedidoRepository = new ItemDoPedidoRepository(dbContexto);

                Console.WriteLine("Opção de exclusão de pedido selecionada.");
                Console.WriteLine("Digite o código do pedido:");
                int pedidoId = int.Parse(Console.ReadLine());

                var pedidoEncontrado = pedidoRepository.ConsultarPedido(pedidoId);

                pedidoRepository.ExcluirPedido(pedidoEncontrado);
            }
        }
    }
}

