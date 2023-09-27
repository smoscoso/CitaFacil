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
        public async Task<Registrar_Cliente> GetRegistrar_Cliente(string Correo, string clave)
        {
            Registrar_Cliente clienteEncontrado = await _context.Registrar_Cliente.Where(u => u.Correo == Correo && u.passsword == clave).FirstOrDefaultAsync();
            return clienteEncontrado;
        }
        public async Task<Registrar_Cliente> SaveRegistrar_Cliente(Registrar_Cliente modeloC)
        {
            _context.Registrar_Cliente.Add(modeloC);
            await _context.SaveChangesAsync();
            return modeloC;
        }

        public async Task<Registrar_Negocio> GetRegistrar_Negocio(string Correo, string clave)
        {
            Registrar_Negocio negocioEncontrado = await _context.Registrar_Negocio.Where(u => u.Correo == Correo && u.passsword == clave).FirstOrDefaultAsync();
            return negocioEncontrado;
        }
        public async Task<Registrar_Negocio> SaveRegistrar_Negocio(Registrar_Negocio modeloN)
        {
            _context.Registrar_Negocio.Add(modeloN);
            await _context.SaveChangesAsync();
            return modeloN;
        }
    }
}
