using System;
using ConsoleAppPedidos.Data;
using ConsoleAppPedidos.Data.Repositories;
using ConsoleAppPedidos.Models;

namespace ConsoleAppPedidos.Services
{
    public class ProdutoService
    {
        public void CriarProduto()
        {
            Console.WriteLine("Opção de criação de produto selecionada.");

            using (var dbContexto = new DBContexto())
            {
                var produtoRepository = new ProdutoRepository(dbContexto);

                Console.WriteLine("###################################");
                Console.WriteLine("       Cadastrando novo produto...      ");
                Console.WriteLine("###################################");

                string adicionarNovoProduto;
                var novoProduto = new Produto();

                do
                {
                    Console.Write("Descrição do Produto: ");
                    string nome = Console.ReadLine();
                    Console.Write("Categoria (0 ou 1):");
                    int categoria = int.Parse(Console.ReadLine());

                    novoProduto = new Produto
                    {
                        Nome = nome,
                        Categoria = categoria
                    };

                    produtoRepository.CriarProduto(novoProduto);

                    Console.Write("Deseja adicionar novo produto? (y/n):");
                    adicionarNovoProduto = Console.ReadLine();

                } while (adicionarNovoProduto.Equals("y"));

                Console.WriteLine("###################################");
                Console.WriteLine("     Produto(s) criado(s) com sucesso!    ");
                Console.WriteLine("###################################");

                ConsultarProduto(novoProduto.ID);
            }
        }

        /// <summary>
        /// Consulta um produto pelo seu ID.
        /// </summary>
        public void ConsultarProduto(int produtoId = 0)
        {
            using (var dbContexto = new DBContexto())
            {
                var produtoRepository = new ProdutoRepository(dbContexto);
                
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

                    Console.WriteLine("###################################");

                    Console.Write(
                        $"Cód. Produto: {produto.ID} | " +
                        $"Descrição: {produto.Nome} | " +
                        $"Categoria: {HelpTools.CarregarCategoriaProduto(produtoId)}\n");

                    Console.WriteLine("###################################");

                    Console.Write("Deseja consultar novo produto? (y/n):");
                    consultarNovoProduto = Console.ReadLine();

                    produtoId = 0;

                } while (consultarNovoProduto.Equals("y"));
            }
        }

        public void ExcluirProduto()
        {
            using (var dbContexto = new DBContexto())
            {
                var produtoRepository = new ProdutoRepository(dbContexto);

                Console.WriteLine("Opção de exclusão do produto selecionado.");
                Console.WriteLine("Digite o código do produto:");
                int produtoId = int.Parse(Console.ReadLine());

                var produtoEncontrado = produtoRepository.ConsultarProduto(produtoId);

                produtoRepository.ExcluirProduto(produtoEncontrado);

                Console.WriteLine("Produto excluído com sucesso.");
            }
        }

        public void AlterarProduto()
        {
            using (var dbContexto = new DBContexto())
            {
                var produtoRepository = new ProdutoRepository(dbContexto);

                var pedidoRepository = new PedidoRepository(dbContexto);
                var itemPedidoRepository = new ItemDoPedidoRepository(dbContexto);

                Console.WriteLine("Opção de atualização do produto selecionado.");

                Console.WriteLine("Digite o código do produto:");
                int produtoId = int.Parse(Console.ReadLine());

                bool produtoComPedido = itemPedidoRepository.ConsultarTodosItensDePedido().AsQueryable().Any(i => i.ProdutoID == produtoId);

                if (!produtoComPedido)
                {
                    Console.Clear();
                    ConsultarProduto(produtoId);

                    Console.WriteLine("Digite o novo Identificador:");
                    string identificador = Console.ReadLine();
                    Console.WriteLine("Digite a nova descrição:");
                    string descricao = Console.ReadLine();
                    Console.WriteLine("Digite o valor total:");
                    double valorTotal = int.Parse(Console.ReadLine());

                    var produtoEncontrado = produtoRepository.ConsultarProduto(produtoId);

                    var produto = new Produto
                    {
                        ID = produtoEncontrado.ID,
                        Nome = produtoEncontrado.Nome,
                        Categoria = produtoEncontrado.Categoria
                    };

                    produtoRepository.AlterarProduto(produto);

                    Console.WriteLine("Produto alterado com sucesso.");
                }
                else
                {
                    Console.WriteLine("Alteração não permitida. Categoria de produto presente em pedido.");
                }
            }
        }
    }
}

