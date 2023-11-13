using System.ComponentModel.DataAnnotations;

namespace CitaFacil.Models
{
    public class Consultores
    {
        [Key]
        public int Id_Consultor { get; set; }
        [Required, StringLength(80)]
        public string Nombre { get; set; }
        [Required, StringLength(80)]
        public string Area { get; set; }
        [Required, StringLength(190)]
        public string Detalle { get; set; }
    }
}
