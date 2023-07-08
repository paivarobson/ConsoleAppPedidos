﻿using ConsoleAppPedidos.Data;
using ConsoleAppPedidos.Data.Repositories;
using ConsoleAppPedidos.Models;

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
            Console.WriteLine("Opção de criação de pedido selecionada.");

            Console.WriteLine("###################################");
            Console.WriteLine("       Criando novo pedido...      ");
            Console.WriteLine("###################################");

            string identificador = pedidoRepository.GerarIdentificadorDoPedido();
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
                    quantidade = int.Parse(Console.ReadLine());
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

            novoPedido.ValorTotal = pedidoRepository.CalcularValorTotal(novoPedido.ID);

            dbContexto.SaveChanges();

            Console.WriteLine("###################################");
            Console.WriteLine("     Pedido criado com sucesso!    ");
            Console.WriteLine("###################################");

            ConsultarPedido(novoPedido.ID);
        }

        /// <summary>
        /// Consulta um pedido pelo seu ID.
        /// </summary>
        /// <param name="pedidoId">ID do pedido a ser consultado.</param>
        public void ConsultarPedido(int pedidoId = 0)
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

                var pedido = pedidoRepository.ConsultarPedido(pedidoId);
                var itensDoPedido = itemPedidoRepository.ConsultarItensDoPedido(pedidoId);
                var produtos = produtoRepository.ConsultarProdutos().ToList();

                int contador = 1;

                if (pedido != null)
                {
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
                else
                {
                    Console.WriteLine("Pedido não localizado.");
                }

                pedidoId = 0;

                Console.Write("Deseja consultar novo pedido? (y/n):");
                consultarNovoPedido = Console.ReadLine();

            } while (consultarNovoPedido.Equals("y"));
        }

        /// <summary>
        /// Altera um pedido existente.
        /// </summary>
        public void AlterarPedido()
        {
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

        /// <summary>
        /// Exclui um pedido existente e seus itens associados.
        /// </summary>
        public void ExcluirPedido()
        {
            Console.WriteLine("Opção de exclusão de pedido selecionada.");
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

                Console.WriteLine("Pedido excluído com sucesso.");
            }
            else
            {
                Console.WriteLine("Pedido não localizado.");
            }
        }
    }
}
