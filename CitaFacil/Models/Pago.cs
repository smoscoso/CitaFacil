using System.ComponentModel.DataAnnotations;

namespace CitaFacil.Models
{
    public class Pago
    {
        [Key]
        public int Id_Pago { get; set; }
        [Required]
        public double Monto { get; set; }
        [Required]
        public string Metodo { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required]
        public string Detalle { get; set; }
    }
}
