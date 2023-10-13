using System.ComponentModel.DataAnnotations;

namespace CitaFacil.Models
{
    public class Suscripcion
    {
        [Key]
        public int Id_Suscripcion { get; set; }
        [Required]
        public DateTime Fecha_Pago { get; set; }
        [Required]
        public DateTime Fecha_Vencimiento { get; set; }
        [Required]
        public string Plan { get; set; }
    }
}
