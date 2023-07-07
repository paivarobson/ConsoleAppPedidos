using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleAppPedidos.Models
{
    /// <summary>
    /// Classe Produto.
    /// </summary>
    [Table("Produtos")]
    public class Produto
    {
        /// <summary>
        /// ID do Produto.
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// Nome do Produto.
        /// </summary>
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(255, ErrorMessage = "O campo Nome deve ter no máximo 255 caracteres.")]
        [Column(TypeName = "varchar")]
        public string Nome { get; set; }
        /// <summary>
        /// Categoria do Produto.
        /// </summary>
        [Required(ErrorMessage = "O campo Categoria é obrigatório.")]
        [Column(TypeName = "tinyint")]
        public int Categoria { get; set; }
    }
}

