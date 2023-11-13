using CitaFacil.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace CitaFacil.Data
{
    public class CitaFacilContext : DbContext
    {
        public CitaFacilContext(DbContextOptions<CitaFacilContext> options):base(options) { }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Empresas> Empresa { get; set; }
        public DbSet<Roles> Rol { get; set; }
        public DbSet<Estados> Estado { get; set; }
        public DbSet<Suscripcion> Suscripcion { get; set; }
        public DbSet<Pago> Pago { get; set; }
        public DbSet<Citas> Cita { get; set; }
<<<<<<< HEAD
        public DbSet<Fecha> Fecha { get; set; }
        public DbSet<ServiciosNegocio>ServiciosN{get; set; }
=======
>>>>>>> c56473f1decee8c3559ed27d8d1f1207e063efde

    }
}
