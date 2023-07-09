using ConsoleAppPedidos.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppPedidos.Data.Repositories
{
    /// <summary>
    /// Classe repositório para manipulação de dados da entidade Pedido.
    /// </summary>
    public class PedidoRepository
    {
        /// <summary>
        /// Propriedade contexto do banco de dados usado para acessar os pedidos.
        /// </summary>
        private readonly AppDbContexto dbContexto;

        /// <summary>
        /// Construtor da classe PedidoRepository.
        /// </summary>
        /// <param name="dbContexto">Contexto do banco de dados.</param>
        /// <exception cref="ArgumentNullException">Exceção lançada quando o dbContexto é nulo.</exception>
        public PedidoRepository(AppDbContexto dbContexto)
        {
            this.dbContexto = dbContexto ?? throw new ArgumentNullException(nameof(dbContexto));
        }

        /// <summary>
        /// Cria um novo pedido.
        /// </summary>
        /// <param name="pedido">O pedido a ser criado.</param>
        /// <exception cref="DbUpdateException">Exceção lançada quando ocorre um erro ao criar o pedido no banco de dados.</exception>
        public void CriarPedido(Pedido pedido)
        {
            try
            {
                dbContexto.Pedidos.Add(pedido);
                dbContexto.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Ocorreu um erro ao criar o pedido no banco de dados.", ex);
            }
        }

        /// <summary>
        /// Consulta todos os pedidos.
        /// </summary>
        /// <returns>Uma consulta de todos os pedidos.</returns>
        public IQueryable<Pedido> ConsultarPedidos()
        {
            return dbContexto.Pedidos.AsQueryable();
        }

        /// <summary>
        /// Consulta um pedido pelo ID.
        /// </summary>
        /// <param name="pedidoId">O ID do pedido.</param>
        /// <returns>O pedido encontrado ou null se não encontrado.</returns>
        /// <exception cref="DbUpdateException">Exceção lançada quando ocorre um erro ao consultar o pedido no banco de dados.</exception>
        public Pedido ConsultarPedido(int pedidoId)
        {
            try
            {
                var pedidoEncontrado = dbContexto.Pedidos.FirstOrDefault(p => p.ID == pedidoId);

                if (pedidoEncontrado != null)
                    return pedidoEncontrado;
                else
                    throw new DbUpdateException($"Pedido com ID {pedidoId} não encontrado.");
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Ocorreu um erro ao consultar o pedido no banco de dados.", ex);
            }
        }

        /// <summary>
        /// Altera um pedido existente.
        /// </summary>
        /// <param name="pedido">O pedido com as alterações.</param>
        /// <exception cref="DbUpdateException">Exceção lançada quando ocorre um erro ao alterar o pedido no banco de dados.</exception>
        public void AlterarPedido(Pedido pedido)
        {
            try
            {
                var pedidoEncontrado = dbContexto.Pedidos.FirstOrDefault(p => p.ID == pedido.ID);

                if (pedidoEncontrado != null)
                {
                    dbContexto.Entry(pedidoEncontrado).CurrentValues.SetValues(pedido);
                    dbContexto.SaveChanges();
                }
                else
                {
                    throw new DbUpdateException($"Pedido com ID {pedido.ID} não encontrado.");
                }
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Ocorreu um erro ao alterar o pedido no banco de dados.", ex);
            }
        }

        /// <summary>
        /// Exclui um pedido.
        /// </summary>
        /// <param name="pedido">O pedido a ser excluído.</param>
        /// <exception cref="DbUpdateException">Exceção lançada quando ocorre um erro ao excluir o pedido no banco de dados.</exception>
        public void ExcluirPedido(Pedido pedido)
        {
            try
            {
                var pedidoEncontrado = dbContexto.Pedidos.FirstOrDefault(p => p.ID == pedido.ID);

                if (pedidoEncontrado != null)
                {
                    dbContexto.Pedidos.Remove(pedidoEncontrado);
                    dbContexto.SaveChanges();
                }
                else
                {
                    throw new DbUpdateException($"Pedido com ID {pedido.ID} não encontrado.");
                }
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Ocorreu um erro ao excluir o pedido no banco de dados.", ex);
            }
        }

        /// <summary>
        /// Calcula o valor total de um pedido, somando os valores unitários dos itens do pedido.
        /// </summary>
        /// <param name="pedidoId">O ID do pedido.</param>
        /// <returns>O valor total do pedido.</returns>
        /// <exception cref="DbUpdateException">Exceção lançada quando ocorre um erro ao calcular o valor total do pedido no banco de dados.</exception>
        public double CalcularValorTotal(int pedidoId)
        {
            try
            {
                var pedido = dbContexto.Pedidos.FirstOrDefault(p => p.ID == pedidoId);

                if (pedido != null)
                {
                    double valorTotal = dbContexto.ItensDePedido
                        .Where(i => i.PedidoID == pedidoId)
                        .Sum(i => i.Quantidade * i.Valor);

                    return valorTotal;
                }

                return 0.0;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Ocorreu um erro ao calcular o valor total do pedido no banco de dados.", ex);
            }
        }

        /// <summary>
        /// Gera um identificador único para um novo pedido no formato "P_[letra, seguida de 3 números]_C".
        /// </summary>
        /// <returns>O identificador gerado para o novo pedido.</returns>
        /// <exception cref="DbUpdateException">Exceção lançada quando ocorre um erro ao calcular o valor total do pedido no banco de dados.</exception>
        public string GerarIdentificadorDoPedido()
        {
            try
            {
                // Obtém o último pedido registrado no banco de dados
                var ultimoPedido = dbContexto.Pedidos.OrderByDescending(p => p.ID).FirstOrDefault();

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
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Ocorreu um erro ao gerar o identificador do pedido.", ex);
            }
        }
    }
}
