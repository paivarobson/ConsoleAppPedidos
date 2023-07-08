using ConsoleAppPedidos.Models;

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
        private readonly DBContexto dbContexto;

        /// <summary>
        /// Construtor da classe PedidoRepositories.
        /// </summary>
        /// <param name="dbContexto">Contexto do banco de dados.</param>
        public PedidoRepository(DBContexto dbContexto)
        {
            this.dbContexto = dbContexto;
        }

        /// <summary>
        /// Método para criar um novo pedido.
        /// </summary>
        /// <param name="pedido">O pedido que será criado.</param>
        public void CriarPedido(Pedido pedido)
        {
            dbContexto.Pedidos.Add(pedido);

            dbContexto.SaveChanges();
        }

        /// <summary>
        /// Método para carregar todos os pedidos.
        /// </summary>
        /// <returns>Retorna todos os pedidos. Usado IQueryable para consulta ser realizado diretamente no banco de dados.</returns>
        public IQueryable<Pedido> ConsultarPedidos()
        {
            return dbContexto.Pedidos.AsQueryable();
        }

        /// <summary>
        /// Método para carregar um pedido com base no ID.
        /// </summary>
        /// <param name="pedidoId">O ID do pedido que será carregado.</param>
        /// <returns>Retorna o pedido ou null se não encontrado.</returns>
        public Pedido ConsultarPedido(int pedidoId)
        {
            return dbContexto.Pedidos.FirstOrDefault(p => p.ID == pedidoId);
        }

        /// <summary>
        /// Método para alterar um pedido existente.
        /// </summary>
        /// <param name="pedido">O pedido que será atualizado.</param>
        public void AlterarPedido(Pedido pedido)
        {
            var pedidoEncontrado = dbContexto.Pedidos.FirstOrDefault(p => p.ID == pedido.ID);

            if (pedidoEncontrado != null)
            {
                dbContexto.Entry(pedidoEncontrado).CurrentValues.SetValues(pedido);

                dbContexto.SaveChanges();
            }
        }

        /// <summary>
        /// Método para excluir um pedido.
        /// </summary>
        /// <param name="pedido">O pedido que será excluído.</param>
        public void ExcluirPedido(Pedido pedido)
        {
            dbContexto.Pedidos.Remove(pedido);

            dbContexto.SaveChanges();
        }

        /// <summary>
        /// Calcula o valor total de um pedido, somando os valores unitários dos itens do pedido.
        /// </summary>
        /// <param name="pedidoId">ID do pedido.</param>
        /// <returns>Retorna o valor total do pedido.</returns>
        public double CalcularValorTotal(int pedidoId)
        {
            var pedido = dbContexto.Pedidos.FirstOrDefault(p => p.ID == pedidoId);

            if (pedido != null)
            {
                // Calcula o valor total do pedido somando os valores unitários de todos os itens do pedido
                var valorTotal = dbContexto.ItensDePedido
                    .Where(i => i.PedidoID == pedidoId)
                    .Sum(i => i.Quantidade * i.Valor);

                return valorTotal;
            }

            return 0.0;
        }

        /// <summary>
        /// Gera um identificador único para um novo pedido no formato "P_[letra, seguida de 3 números]_C".
        /// </summary>
        /// <returns>Retorna o identificador gerado para o novo pedido.</returns>
        public string GerarIdentificadorDoPedido()
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

    }
}
