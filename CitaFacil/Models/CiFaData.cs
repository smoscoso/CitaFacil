using CitaFacil.Data;
using Microsoft.EntityFrameworkCore;

namespace CitaFacil.Models
{
    public class CiFaData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CitaFacilContext(
                serviceProvider.GetRequiredService<DbContextOptions<CitaFacilContext>>()))
            {
                ServiciosCitaFacil servicios = new ServiciosCitaFacil(context);
                if (context.Estado.Any() || context.Registrar_Negocio.Any() || context.Roles.Any())
                {
                    return;
                }
                if (context.Estado.Any() || context.Registrar_Cliente.Any() || context.Roles.Any())
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
                context.Roles.AddRange(
                    new Roles
                    {
                        Nombre_Rol = "Cliente"
                    },
                    new Roles
                    {
                        Nombre_Rol = "Negocio"
                    },
                    new Roles
                    {
                        Nombre_Rol = "Administrador"
                    }
                 );
                context.SaveChanges();
                _ = context.Registrar_Cliente.Add(new Registrar_Cliente
                {
                    Primer_Nombre = "Sergio",
                    Segundo_Nombre = "Leonardo",
                    Primer_Apellido = "Moscoso",
                    Segundo_Apellido = "Ramirez",
                    Correo = "sergiomoscoso1022@hotmail.com",
                    Telefono = "3118613395",
                    Telefono_Fijo = "3118613395",
                    Cedula = "1193224152",
                    Id_Rol = 3
                });
                context.SaveChanges();
            }
        }
    }
}
