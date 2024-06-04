using System.ComponentModel.DataAnnotations;

namespace EcoWater.Models
{
    public class Sensores
    {
        [Key]
        public int Id_Sensor { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public string Localizacao { get; set; }
        [Required]
        public string Data_Instalacao { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
