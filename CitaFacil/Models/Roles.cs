using System.ComponentModel.DataAnnotations;

namespace CitaFacil.Models
{
    public class Roles
    {
        [Key]
        public int Id_Rol { get; set; }
        [Required, StringLength(50)]
        public string Nombre_Rol { get; set; }
    }
}
