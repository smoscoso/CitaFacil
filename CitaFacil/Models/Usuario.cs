using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitaFacil.Models
{
    public class Usuario
    {
        [Key]
        [StringLength(80)]
        public int Id_Usuario { get; set; }
        [Required,MinLength(3), StringLength(80)]
        public string Nombre_Usuario { get; set; }
        [StringLength(80)]
        public string Cedula { get; set; }
        [Required]
        public string Correo { get; set; }
        [Required, StringLength(80)]
        public string Telefono { get; set; }
        [Required, StringLength(80)]
        public string Telefono_Fijo { get; set; }
        
        [Required,MinLength(8), Column(TypeName ="nvarchar(MAX)")]
        public string contraseña { get; set; }
        [ForeignKey("Rol")]
        public int Id_Rol { get; set; }
        public virtual Roles? Rol { get; set; }
    }
}
