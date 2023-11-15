using System.ComponentModel.DataAnnotations;

namespace CitaFacil.Models
{
    public class ServiciosNegocio
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
