namespace ConsoleAppPedidos.Models
{
    /// <summary>
    /// Classe Pedido.
    /// </summary>
    public class Pedido
    {
        /// <summary>
        /// ID do Pedido.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Identificador do Pedido.
        /// </summary>
        public string Identificador { get; set; }
        /// <summary>
        /// Descrição do Pedido.
        /// </summary>
        public string? Descricao { get; set; }
        /// <summary>
        /// Valor total do Pedido.
        /// </summary>
        public decimal ValorTotal { get; set; }
    }
}

