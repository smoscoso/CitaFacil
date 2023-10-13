
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitaFacil.Models
{
    public class Citas
    {
        [Key]
        public int Id_Cita { get; set; }
        [Required]
        public DateTime Fecha_Cita { get; set; }
        [Required]
        public DateTime Hora_Cita { get; set; }
        [Required]
        public DateTime Duracion_Cita { get; set; }
        [Required]
        public string Motivo_Cita { get; set; }
        [Required]
        public string ubicacion { get; set; }
        [Required]
        public string Detalle { get; set; }
        [Required]
        public Boolean Estado { get; set; }

    }
}
