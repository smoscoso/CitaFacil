using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitaFacil.Models
{
    public class Usuario
    {
        [Key]
        public int Id_Usuario { get; set; }
        [Required,MinLength(3), StringLength(100)]
        public string Nombre { get; set; }
        [StringLength(80)]
        public string Cedula { get; set; }
        [Required]
        public string Correo { get; set; }
        [Required, StringLength(80)]
        public string Celular { get; set; }
        [Required, StringLength(80)]
        public string? Telefono { get; set; }
        
        [Required,MinLength(8), Column(TypeName ="nvarchar(MAX)")]
        public string contraseña { get; set; }

        [ForeignKey("Roles")]
        public int Id_Rol { get; set; }
        public virtual Roles? Roles { get; set; }

        [ForeignKey("Estados")]
        public int Id_EstadoUsuario { get; set; }
        public virtual Estados? Estados { get; set;}
    }
}
