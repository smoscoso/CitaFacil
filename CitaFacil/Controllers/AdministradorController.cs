using CitaFacil.Data;
using CitaFacil.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CitaFacil.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdministradorController : Controller
    {
        private readonly CitaFacilContext _context;

        public AdministradorController(CitaFacilContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListadoUsuarioEmpresa()
        {
            List<Usuario> listaUsuarios = _context.Usuario
                .Where(u => u.Id_Rol == 3)
                .Include(u => u.Estados)
                .ToList();
            ViewBag.listaUsuarios = listaUsuarios;

            List<Empresas> listaEmpresas = _context.Empresa
                .Where(u => u.Id_Rol == 3)
                .Include(u => u.Estados)
                .ToList();
            ViewBag.listaEmpresas = listaEmpresas;
            return View();
        }

        public IActionResult AceptarUsuario(int id)
        {
            Usuario usuario = _context.Usuario.FirstOrDefault(u => u.Id_Usuario == id);
            usuario.Id_EstadoUsuario = 5;
            _context.SaveChanges();
            return RedirectToAction("ListadoUsuarioEmpresa");
        }
            
        public IActionResult RechazarUsuario(int id)
        {
            Usuario usuario = _context.Usuario.FirstOrDefault(u => u.Id_Usuario == id);
            usuario.Id_EstadoUsuario = 6;
            _context.SaveChanges();
            return RedirectToAction("ListadoUsuarioEmpresa");
        }

        public IActionResult AceptarEmpresa(int id)
        {
            Empresas empresa = _context.Empresa.FirstOrDefault(u => u.Id_Empresa == id);
            empresa.Id_EstadoEmpresa = 5;
            _context.SaveChanges();
            return RedirectToAction("ListadoUsuarioEmpresa");
        }

        public IActionResult RechazarEmpresa(int id)
        {
            Empresas empresa = _context.Empresa.FirstOrDefault(u => u.Id_Empresa == id);
            empresa.Id_EstadoEmpresa = 6;
            _context.SaveChanges();
            return RedirectToAction("ListadoUsuarioEmpresa");
        }

    }
}
