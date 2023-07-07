using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleAppPedidos.Models
{
    /// <summary>
    /// Classe Item do Pedido.
    /// </summary>
    [Table("ItensDePedido")]
    public class ItensDePedido
    {
        /// <summary>
        /// ID do Item do Pedido.
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// ID do Pedido associado ao Item do Pedido.
        /// </summary>
        [Required(ErrorMessage = "O campo ID do Pedido é obrigatório")]
        [ForeignKey("Pedido")]
        public int PedidoID { get; set; }
        /// <summary>
        /// ID do Produto associado ao Item do Pedido.
        /// </summary>
        [Required(ErrorMessage = "O campo ID do Produto é obrigatório")]
        [ForeignKey("Produto")]
        public int ProdutoID { get; set; }
        /// <summary>
        /// Quantidade do Produto do Item do Pedido.
        /// </summary>

        [Required(ErrorMessage = "O campo Quantidade é obrigatório")]
        public int Quantidade { get; set; }
        /// <summary>
        /// Valor do Item do Pedido.
        /// </summary>
        [Required(ErrorMessage = "O campo Valor é obrigatório")]
        [Column(TypeName = "decimal(9,2)")]
        public int Valor { get; set; }
    }
}

