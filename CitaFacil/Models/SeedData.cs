using CitaFacil.Data;
using Microsoft.EntityFrameworkCore;

namespace CitaFacil.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CitaFacilContext(
                serviceProvider.GetRequiredService<DbContextOptions<CitaFacilContext>>()))
            {
                ServiciosCitaFacil servicios = new ServiciosCitaFacil(context);

                if (context.Estado.Any()||context.Rol.Any())
                {
                    return;
                }
                context.Estado.AddRange(
                   new Estados
                   {
                       Estado = "Disponible"
                   },
                   new Estados
                   {
                       Estado = "Ocupado"
                   },
                   new Estados
                   {
                       Estado = "Cancelado"
                   },
                   new Estados
                   {
                       Estado = "En Revison"
                   },
                   new Estados
                   {
                       Estado = "Aceptado"
                   },
                   new Estados
                   {
                       Estado = "Rechazado"
                   }
                );
                context.SaveChanges();
                context.Rol.AddRange(
                    new Roles
                    {
                        Nombre_Rol = "Administrador"
                    },
                    new Roles
                    {
                        Nombre_Rol = "Empresa"
                    },
                    new Roles
                    {
                        Nombre_Rol = "Usuario"
                    },
                    new Roles
                    {
                        Nombre_Rol = "Consultores"
                    }
                 );
                context.SaveChanges();
                context.Usuario.Add(new Usuario
                {
                    Nombre = "Sergio Leonardo Moscoso Ramirez",
                    contraseña = servicios.HashPassword("12345678"),
                    Celular = "3118613395",
                    Telefono = "3118613395",
                    Cedula = "1193224152",
                    Id_Rol = 1,
                    Id_EstadoUsuario = 5,
                    Correo = "sergiomoscoso1022@hotmail.com"
                });
                context.SaveChanges();
            }
        }
    }
}
