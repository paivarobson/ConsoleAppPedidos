namespace ConsoleAppPedidos.Models
{
    /// <summary>
    /// Classe Item do Pedido.
    /// </summary>
    public class ItemPedido
    {
        /// <summary>
        /// ID do Item do Pedido.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// ID do Pedido associado ao Item do Pedido.
        /// </summary>
        public int PedidoID { get; set; }
        /// <summary>
        /// ID do Produto associado ao Item do Pedido.
        /// </summary>
        public int ProdutoID { get; set; }
        /// <summary>
        /// Quantidade do Produto do Item do Pedido.
        /// </summary>
        public int Quantidade { get; set; }
        /// <summary>
        /// Valor do Item do Pedido.
        /// </summary>
        public int Valor { get; set; }
    }
}

