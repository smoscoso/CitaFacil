using System.ComponentModel.DataAnnotations;

namespace CitaFacil.Models.DTO
{
    public class EmpresaDTO
    {
        [Required]
        [MinLength(3)]
        public string Nombre { get; set; }
        [Required]
        public string direccion { get; set; }
        [Required]
        public string servicio { get; set; }
        public long NIT { get; set; }
        [Required]
        public string correo { get; set; }
        [MinLength(8)]
        public string telefonoC { get; set; }
        [MinLength(8)]
        public string telefonoF { get; set; }

        [Required]
        [MinLength(8)]
        public string contrasena { get; set; }
        [Required]
        [MinLength(8)]
        public string contrasenaConf { get; set; }
    }
}
