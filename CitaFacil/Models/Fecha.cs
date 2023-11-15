using System.ComponentModel.DataAnnotations;

namespace CitaFacil.Models
{
    public class Fecha
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Fecha obligatoria")]
        public DateTime fecha { get; set; }
    }
}
