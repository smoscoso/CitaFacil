using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitaFacil.Models
{
    public class Registrar_Cliente
    {
        [Key]
        [StringLength(80)]
        public string Correo { get; set; }
        [Required,MinLength(3), StringLength(80)]
        public string Primer_Nombre { get; set; }
        [Required, MinLength(3), StringLength(80)]
        public string Segundo_Nombre { get; set; }
        [Required, MinLength(3), StringLength(80)]
        public string Primer_Apellido { get; set; }
        [Required, MinLength(3), StringLength(80)]
        public string Segundo_Apellido { get; set; }
        [Required, StringLength(80)]
        public string Telefono { get; set; }
        [Required, StringLength(80)]
        public string Telefono_Fijo { get; set; }
        [StringLength(80)]
        public string Cedula { get; set; }
        [Required,MinLength(8), Column(TypeName ="nvarchar(MAX)")]
        public string passsword { get; set; }
        [ForeignKey("Roles")]
        public int Id_Rol { get; set; }
    }
}
