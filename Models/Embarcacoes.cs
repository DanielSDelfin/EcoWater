using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoWater.Models
{
    public class Embarcacoes
    {
        [Key]
        public int Id_Embarcacao { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public string Bandeira { get; set; }
        [Required]
        public double Capacidade { get; set; }
        [Required]
        public int Ano_Fabricação { get; set; }

        // Removido o atributo ForeignKey aqui, pois ele será usado na propriedade Proprietarios
        public int? Id_Proprietario { get; set; } // Fazendo o Id_Proprietario opcional

        [ForeignKey("Id_Proprietario")]
        public virtual Proprietarios Proprietarios { get; set; } // Agora é virtual para suportar lazy loading
    }
}