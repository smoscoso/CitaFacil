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
                if (context.Estado.Any()||context.Usuario.Any() || context.Rol.Any())
                {
                    return;
                }
                context.Estado.AddRange(
                   new Estado
                   {
                       estado = "Disponible"
                   },
                   new Estado
                   {
                       estado = "Ocupado"
                   },
                   new Estado
                   {
                       estado = "Cancelado"
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
                    Id_Usuario = 1,
                    Nombre_Usuario = "Sergio Leonardo Moscoso Ramirez",
                    contraseña = servicios.CifrarContraseña("12345678"),
                    Telefono = "3118613395",
                    Telefono_Fijo = "3118613395",
                    Cedula = "1193224152",
                    Id_Rol = 1
                });
                context.SaveChanges();
            }
        }
    }
}
