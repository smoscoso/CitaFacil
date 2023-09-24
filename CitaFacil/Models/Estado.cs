using System.ComponentModel.DataAnnotations;

namespace CitaFacil.Models
{
    public class Estado
    {
        [Key]
        public int Id_Estado { get; set; }
        [Required, StringLength(20)]
        public string estado { get; set; }
    }
}
