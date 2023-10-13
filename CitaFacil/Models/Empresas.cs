using Org.BouncyCastle.Asn1.Cms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitaFacil.Models
{
    public class Empresas
    {
        [Key]
        public int Id_Empresa { get; set; }
        [Required, StringLength(80)]
        public string Correo { get; set; }
        [StringLength(80)]
        public string Nombre_Empresa { get; set; }
        [Required, StringLength(80)]
        public string ubicacion { get; set; }
        [Required, StringLength(80)]
        public string tipo_Empresa { get; set; }
        [Required]
        public char plan { get; set; }
        [Required]
        public DateTime fecha_Registro { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required, MinLength(8), Column(TypeName = "nvarchar(MAX)")]
        public string contraseña { get; set; }

        [ForeignKey("Rol")]
        public int Id_Rol { get; set; }
        public virtual Roles? Rol { get; set; }
    }
}
