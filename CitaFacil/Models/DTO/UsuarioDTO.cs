using System.ComponentModel.DataAnnotations;

namespace CitaFacil.Models.DTO
{
    public class UsuarioDTO
    {
        [Required]
        [MinLength(3)]
        public string primerNombre { get; set; }
        [MinLength(3)]
        public string segundoNombre { get; set; }
        [Required]
        [MinLength(3)]
        public string primerApellido { get; set; }
        [MinLength(3)]
        public string segundoApellido { get; set; }
        [Required]
        public string cedula { get; set; }
        [Required]
        public string correo { get; set; }
        [MinLength(8)]
        public string telefonoC { get; set; }
        [MinLength(8)]
        public string telefonoF { get; set; }
        
        [Required][MinLength(8)]
        public string contrasena { get; set; }
        [Required][MinLength(8)]
        public string contrasenaConf { get; set; }

    }
}
