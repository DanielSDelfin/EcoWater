using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoWater.Models
{
    [Table("Proprietarios")]
    public class Proprietarios
    {
        [Key]
        public int Id_Proprietario { get; set; }
        [Required]
        public string? Nome { get; set; }
        [Required]
        public string? Endereco { get; set; }
        [Required]
        public string? Telefone { get; set; }
        [Required]
        public string? Email { get; set; }

        [InverseProperty("Proprietarios")]
        public ICollection<Embarcacoes> Embarcacoes { get; set; } = new List<Embarcacoes>();
    }
}