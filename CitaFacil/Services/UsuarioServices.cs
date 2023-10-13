using Microsoft.EntityFrameworkCore;
using CitaFacil.Models;
using CitaFacil.Services;
using CitaFacil.Data;

namespace CitaFacil.Services
{
    public class UsuarioServices : IUsuarioService
    {
        private readonly CitaFacilContext _context;
        public UsuarioServices(CitaFacilContext context)
        {
            _context= context;
        }
        public async Task<Usuario> GetRegistrar_Cliente(string Correo, string clave)
        {
            Usuario clienteEncontrado = await _context.Usuario.Where(u => u.Correo == Correo && u.contraseña == clave).FirstOrDefaultAsync();
            return clienteEncontrado;
        }
        public async Task<Usuario> SaveRegistrar_Cliente(Usuario modeloC)
        {
            _context.Usuario.Add(modeloC);
            await _context.SaveChangesAsync();
            return modeloC;
        }

        public async Task<Empresas> GetRegistrar_Negocio(string Correo, string clave)
        {
            Empresas negocioEncontrado = await _context.Empresa.Where(u => u.Correo == Correo && u.contraseña == clave).FirstOrDefaultAsync();
            return negocioEncontrado;
        }
        public async Task<Empresas> SaveRegistrar_Negocio(Empresas modeloN)
        {
            _context.Empresa.Add(modeloN);
            await _context.SaveChangesAsync();
            return modeloN;
        }
    }
}
