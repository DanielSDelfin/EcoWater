using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoWater.Models
{
    public class Monitoramentos
    {
        [Key]
        public int Id_Monitoramento { get; set; }
        [Required]
        [ForeignKey("Embarcacoes")]
        public int Id_Embarcacao { get; set; }
        [Required]
        [ForeignKey("Sensores")]
        public int Id_Sensor { get; set; }
        [Required]
        public string Data { get; set; }
        [Required]
        public string Hora { get; set; }
        [Required]
        public string Localizacao { get; set; }
        [Required]
        public string Nivel_Poluicao { get; set; }

        public Embarcacoes Embarcacoes { get; set; }

        public Sensores Sensores { get; set; }
    }
}
