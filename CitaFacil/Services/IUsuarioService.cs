using CitaFacil.Models;
using Microsoft.EntityFrameworkCore;

namespace CitaFacil.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> GetRegistrar_Cliente(string Correo, string clave);
        Task<Usuario> SaveRegistrar_Cliente(Usuario modeloC);
        Task<Empresas> GetRegistrar_Negocio(string Correo, string clave);
        Task<Empresas> SaveRegistrar_Negocio(Empresas modeloN);
    }
}
