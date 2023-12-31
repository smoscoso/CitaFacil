﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitaFacil.Models
{
    public class Registrar_Negocio
    {
        [Key]
        [StringLength(80)]
        public string Correo { get; set; }
        [StringLength(80)]
        public string Nombre { get; set; }
        [Required, StringLength(80)]
        public int Telefono { get; set; }
        [Required, StringLength(80)]
        public int Telefono_Fijo { get; set; }

        [Required, StringLength(80)]
        public string Direccion { get; set; }
        [Required, StringLength(240)]
        public string Descripcion { get; set; }
        [Required, MinLength(8), Column(TypeName = "nvarchar(MAX)")]
        public string passsword { get; set; }

        [ForeignKey("Roles")]
        public int Id_Rol { get; set; }
        public virtual Roles? Rol { get; set; }
    }
}
