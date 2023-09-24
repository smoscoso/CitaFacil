using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace CitaFacil.Data
{
    public class CitaFacilContext : DbContext
    {
        public CitaFacilContext(DbContextOptions<CitaFacilContext> options):base(options) { }
        public DbSet<Models.Registrar_Cliente> Registrar_Cliente { get; set; }
        public DbSet<Models.Registrar_Negocio> Registrar_Negocio { get; set; }
        public DbSet<Models.Roles> Roles { get; set; }
        public DbSet<Models.Estado> Estado { get; set; }

    }
}
