
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitaFacil.Models
{
    public class Citas
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public DateTime Hora { get; set; }
        [Required]
        public DateTime Duracion { get; set; }
        [Required]
        public string Motivo { get; set; }
        [Required]
        public string ubicacion { get; set; }
        [Required]
        public string Detalle { get; set; }
        [Required]
        public Boolean Estado { get; set; }

    }
}
