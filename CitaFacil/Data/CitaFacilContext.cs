using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace CitaFacil.Data
{
    public class CitaFacilContext : DbContext
    {
        public CitaFacilContext(DbContextOptions<CitaFacilContext> options):base(options) { }
        public DbSet<Models.Usuario> Usuario { get; set; }
        public DbSet<Models.Empresas> Empresa { get; set; }
        public DbSet<Models.Roles> Rol { get; set; }
        public DbSet<Models.Estado> Estado { get; set; }
        public DbSet<Models.Suscripcion> Suscripcion { get; set; }
        public DbSet<Models.Pago> Pago { get; set; }
        public DbSet<Models.Citas> Cita { get; set; }

    }
}
