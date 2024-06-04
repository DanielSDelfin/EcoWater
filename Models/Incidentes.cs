using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoWater.Models
{
    public class Incidentes
    {
        [Key]
        public int Id_Incidente { get; set; }
        [Required]
        [ForeignKey("Embarcacoes")]
        public int Id_Embarcacao { get; set; }
        [Required]
        public string Data { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public string Tipo_Poluicao { get; set; }
        [Required]
        public string Severidade { get; set; }
    }
}
