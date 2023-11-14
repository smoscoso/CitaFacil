using Org.BouncyCastle.Asn1.Cms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitaFacil.Models
{
    public class Empresas
    {
        [Key]
        public int Id_Empresa { get; set; }

        [Required]
        public long NIT {  get; set; }

        [Required, StringLength(80)]
        public string Correo { get; set; }
        [StringLength(80)]
        public string Nombre { get; set; }
        [Required, StringLength(80)]
        public string ubicacion { get; set; }
        [Required, StringLength(80)]
        public string Tipo { get; set; }
        [Required]
        public char plan { get; set; }

        [Required, StringLength(80)]
        public string Celular { get; set; }
        [Required, StringLength(80)]
        public string? Telefono { get; set; }

        [Required, MinLength(8), Column(TypeName = "nvarchar(MAX)")]
        public string contraseña { get; set; }

        [Required]
        public DateTime fecha_Registro { get; set; }

        [ForeignKey("Roles")]
        public int Id_Rol { get; set; }
        public virtual Roles? Roles { get; set; }

        [ForeignKey("Estado")]
        public int Id_EstadoEmpresa { get; set; }
        public virtual Estados Estados { get; set; }
    }
}
