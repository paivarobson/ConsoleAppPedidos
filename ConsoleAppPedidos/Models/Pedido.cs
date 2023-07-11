using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleAppPedidos.Models
{
    /// <summary>
    /// Classe Pedido.
    /// </summary>
    [Table("Pedidos")]
    public class Pedido
    {
        /// <summary>
        /// ID do Pedido.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Identificador do Pedido.
        /// </summary>
        [Required(ErrorMessage = "O campo Identificador é obrigatório.")]
        [StringLength(255, ErrorMessage = "O campo Identificador deve ter no máximo 255 caracteres.")]
        [Column(TypeName = "varchar")]
        public string Identificador { get; set; }

        /// <summary>
        /// Descrição do Pedido.
        /// </summary>
        [StringLength(1000, ErrorMessage = "O campo Descrição deve ter no máximo 1000 caracteres.")]
        [Column(TypeName = "varchar")]
        public string? Descricao { get; set; }

        /// <summary>
        /// Valor total do Pedido.
        /// </summary>
        [Required(ErrorMessage = "O campo Valor Total é obrigatório.")]
        [Column(TypeName = "decimal(21,2)")]
        public double ValorTotal { get; set; }

        /// <summary>
        /// Itens do pedido associados ao pedido.
        /// </summary>
        public ICollection<ItemDoPedido> ItensDoPedido { get; set; }
    }
}

