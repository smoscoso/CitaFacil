using System.ComponentModel.DataAnnotations;

namespace CitaFacil.Models
{
    public class Estados
    {
        [Key]
        public int Id_Estado { get; set; }
        [Required, StringLength(20)]
        public string Estado { get; set; }
    }
}
