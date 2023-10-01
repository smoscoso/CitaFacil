using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CitaFacil.Data;
using CitaFacil.Models;
using CitaFacil.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CitaFacil.Controllers
{
    public class Registrar_ClienteController : Controller
    {
        private readonly CitaFacilContext _context;
        private readonly ServiciosCitaFacil _servicios;
        private readonly IUsuarioService _usuarioServico;

        public Registrar_ClienteController(IUsuarioService usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }
        public IActionResult IniciarSesionCliente()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IniciarSesionCliente(string correo, string clave)
        {
            Registrar_Cliente clienteEncontrado = await _usuarioServico.GetRegistrar_Cliente(correo, ServiciosCitaFacil.CifrarContraseña(clave));
            if (clienteEncontrado != null)
            {
                ViewData["Message"] = "No se encontro en el sistema";
            }
            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, clienteEncontrado.Primer_Nombre, clienteEncontrado.Primer_Apellido),
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), properties);
            return RedirectToAction("Index", "Home");
        }













        // GET: Registrar_Cliente
        public async Task<IActionResult> Index()
        {
              return _context.Registrar_Cliente != null ? 
                          View(await _context.Registrar_Cliente.ToListAsync()) :
                          Problem("Entity set 'CitaFacilContext.Registrar_Cliente'  is null.");
        }

        // GET: Registrar_Cliente/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Registrar_Cliente == null)
            {
                return NotFound();
            }

            var registrar_Cliente = await _context.Registrar_Cliente
                .FirstOrDefaultAsync(m => m.Correo == id);
            if (registrar_Cliente == null)
            {
                return NotFound();
            }

            return View(registrar_Cliente);
        }

        // GET: Registrar_Cliente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Registrar_Cliente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Correo,Primer_Nombre,Segundo_Nombre,Primer_Apellido,Segundo_Apellido,Telefono,Telefono_Fijo,Cedula,passsword,Id_Rol")] Registrar_Cliente registrar_Cliente)
        {
            registrar_Cliente.passsword = ServiciosCitaFacil.CifrarContraseña(registrar_Cliente.passsword);
            Registrar_Cliente clienteCreado = await _usuarioServico.SaveRegistrar_Cliente(registrar_Cliente);
            if (clienteCreado.Correo != null)
            {
                return RedirectToAction("IniciarSesionCliente", "Inicio");
            }
            ViewData["Message"] = "No se pudo Crear el cliente";
            return View();
        }

        // GET: Registrar_Cliente/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Registrar_Cliente == null)
            {
                return NotFound();
            }

            var registrar_Cliente = await _context.Registrar_Cliente.FindAsync(id);
            if (registrar_Cliente == null)
            {
                return NotFound();
            }
            return View(registrar_Cliente);
        }

        // POST: Registrar_Cliente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Correo,Primer_Nombre,Segundo_Nombre,Primer_Apellido,Segundo_Apellido,Telefono,Telefono_Fijo,Cedula,passsword,Id_Rol")] Registrar_Cliente registrar_Cliente)
        {
            if (id != registrar_Cliente.Correo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registrar_Cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Registrar_ClienteExists(registrar_Cliente.Correo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(registrar_Cliente);
        }

        // GET: Registrar_Cliente/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Registrar_Cliente == null)
            {
                return NotFound();
            }

            var registrar_Cliente = await _context.Registrar_Cliente
                .FirstOrDefaultAsync(m => m.Correo == id);
            if (registrar_Cliente == null)
            {
                return NotFound();
            }

            return View(registrar_Cliente);
        }

        // POST: Registrar_Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Registrar_Cliente == null)
            {
                return Problem("Entity set 'CitaFacilContext.Registrar_Cliente'  is null.");
            }
            var registrar_Cliente = await _context.Registrar_Cliente.FindAsync(id);
            if (registrar_Cliente != null)
            {
                _context.Registrar_Cliente.Remove(registrar_Cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Registrar_ClienteExists(string id)
        {
          return (_context.Registrar_Cliente?.Any(e => e.Correo == id)).GetValueOrDefault();
        }
    }
}
