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

        [ForeignKey("Proprietarios")]
        public int Id_Proprietario { get; set; }
        public Proprietarios? Proprietarios { get; set; }

        [InverseProperty("Embarcacoes")]
        public ICollection<RegistrosPoluicao> RegistrosPoluicaos { get; set; } = new List<RegistrosPoluicao>();

        [InverseProperty("Embarcacoes")]
        public ICollection<Incidentes> Incidentes { get; set; } = new List<Incidentes>();
    }

}