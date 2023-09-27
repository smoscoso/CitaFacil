using CitaFacil.Models;
using Microsoft.EntityFrameworkCore;

namespace CitaFacil.Services
{
    public interface IUsuarioService
    {
        Task<Registrar_Cliente> GetRegistrar_Cliente(string Correo, string clave);
        Task<Registrar_Cliente> SaveRegistrar_Cliente(Registrar_Cliente modeloC);
        Task<Registrar_Negocio> GetRegistrar_Negocio(string Correo, string clave);
        Task<Registrar_Negocio> SaveRegistrar_Negocio(Registrar_Negocio modeloN);
    }
}
