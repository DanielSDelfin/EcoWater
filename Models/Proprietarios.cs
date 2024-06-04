using System.ComponentModel.DataAnnotations;

namespace EcoWater.Models
{
    public class Proprietarios
    {
        [Key]
        public int Id_Proprietario { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Endereco { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Required]
        public string Email { get; set; }

        // A propriedade Embarcacoes agora é virtual para suportar lazy loading
        public virtual ICollection<Embarcacoes> Embarcacoes { get; set; } = new HashSet<Embarcacoes>();
    }
}