using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CitaFacil.Data;
using CitaFacil.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CitaFacil.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly CitaFacilContext _context;
        private readonly ServiciosCitaFacil _servicios;

        public UsuarioController(CitaFacilContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Consultar()
        {
            return _context.Usuario != null ?
                          View(await _context.Usuario.Include(u=>u.Rol).ToListAsync()) :
                          Problem("Entity set 'CitaFacilContext.Usuario'  is null.");
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
              return _context.Usuario != null ? 
                          View(await _context.Usuario.ToListAsync()) :
                          Problem("Entity set 'CitaFacilContext.Usuario'  is null.");
        }
        public IActionResult Registro(bool agregado=false)
        {
            return View();
        }

        public async Task<IActionResult> Registrarse([Bind("Id_Usuario","Nombre_Usuario", "Cedula", "Correo", "Telefono", "Telefono_Fijo", "contraseña", "Id_Rol")] Usuario usuario)
        {
            Usuario usuarioExiste=_context.Usuario.FirstOrDefault(u=>u.Correo==usuario.Correo);
            if(ModelState.IsValid && usuarioExiste==null)
            {
                string constraseñaCifrada= _servicios.CifrarContraseña(usuario.contraseña);
                usuario.contraseña=constraseñaCifrada;
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                _servicios.EnviarCorreoUsuario(usuario.Correo);
                return RedirectToAction("Iniciar_Sesion", new {agregado=true});
            }
            if(usuarioExiste!=null)
            {
                return RedirectToAction("Registrarse",new {agregado=true});
            }
            return View(usuario);
        }

        public IActionResult iniciar_Sesion()
        {
            return View();
        }
        public IActionResult Iniciar_Sesion_Cliente(bool agregado = false)
        {
            ViewBag.Agregado = agregado;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>IniciarSesion(string correo, string contraseña)
        {
            Usuario usuariolog;
            if(correo != null && contraseña != null)
            {
                usuariolog = _servicios.comprobarContraseña(correo, contraseña);
                if(usuariolog != null)
                {
                    ClaimsIdentity identity = new ClaimsIdentity("CookieAuth",ClaimTypes.Name, ClaimTypes.Role);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuariolog.Nombre_Usuario),
                        new Claim(ClaimTypes.Role, usuariolog.Rol.Nombre_Rol),
                        new Claim("Correo", usuariolog.Correo)
                    };
                    identity.AddClaim(claims.FirstOrDefault(u=>u.Type==ClaimTypes.Name));
                    identity.AddClaim(claims.FirstOrDefault(u => u.Type == ClaimTypes.Role));
                    identity.AddClaim(claims.FirstOrDefault(u => u.Type == "Correo"));

                    ClaimsPrincipal userprincipal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync("CookieAuth", userprincipal, new AuthenticationProperties
                    {
                        ExpiresUtc= DateTime.Now.AddMinutes(30)
                    });
                    return RedirectToAction("PrincipalAdmin", "usuario");
                }   
            }
            usuariolog=_context.Usuario.Include(u=>u.Rol).FirstOrDefault(u=>u.Correo==correo);
            if(correo!=null && usuariolog!=null && usuariolog.Id_Rol==3)
            {
                ClaimsIdentity identity = new ClaimsIdentity("CookieAuth", ClaimTypes.Name, ClaimTypes.Role);
                var claims = new List<Claim>
                {
                        new Claim(ClaimTypes.Name, usuariolog.Nombre_Usuario),
                        new Claim(ClaimTypes.Role, usuariolog.Rol.Nombre_Rol),
                        new Claim("Correo", usuariolog.Correo)
                };
                identity.AddClaim(claims.FirstOrDefault(u => u.Type == ClaimTypes.Name));
                identity.AddClaim(claims.FirstOrDefault(u => u.Type == ClaimTypes.Role));
                identity.AddClaim(claims.FirstOrDefault(u => u.Type == "Correo"));

                ClaimsPrincipal userPrincipal= new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("CookieAuth", userPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddMinutes(30)
                });
                return RedirectToAction("PrincipalUsuario", "usuario", new {cita=false});
            }
            usuariolog = _context.Usuario.Include(u => u.Rol).FirstOrDefault(u => u.Correo == correo);
            if (correo != null && usuariolog != null && usuariolog.Id_Rol == 2)
            {
                ClaimsIdentity identity = new ClaimsIdentity("CookieAuth", ClaimTypes.Name, ClaimTypes.Role);
                var claims = new List<Claim>
                {
                        new Claim(ClaimTypes.Name, usuariolog.Nombre_Usuario),
                        new Claim(ClaimTypes.Role, usuariolog.Rol.Nombre_Rol),
                        new Claim("Correo", usuariolog.Correo)
                };
                identity.AddClaim(claims.FirstOrDefault(u => u.Type == ClaimTypes.Name));
                identity.AddClaim(claims.FirstOrDefault(u => u.Type == ClaimTypes.Role));
                identity.AddClaim(claims.FirstOrDefault(u => u.Type == "Correo"));

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("CookieAuth", userPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddMinutes(30)
                });
                return RedirectToAction("PrincipalEmpresa", "usuario", new { cita = false });
            }
            usuariolog = _context.Usuario.Include(u => u.Rol).FirstOrDefault(u => u.Correo == correo);
            if (correo != null && usuariolog != null && usuariolog.Id_Rol == 4)
            {
                ClaimsIdentity identity = new ClaimsIdentity("CookieAuth", ClaimTypes.Name, ClaimTypes.Role);
                var claims = new List<Claim>
                {
                        new Claim(ClaimTypes.Name, usuariolog.Nombre_Usuario),
                        new Claim(ClaimTypes.Role, usuariolog.Rol.Nombre_Rol),
                        new Claim("Correo", usuariolog.Correo)
                };
                identity.AddClaim(claims.FirstOrDefault(u => u.Type == ClaimTypes.Name));
                identity.AddClaim(claims.FirstOrDefault(u => u.Type == ClaimTypes.Role));
                identity.AddClaim(claims.FirstOrDefault(u => u.Type == "Correo"));

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("CookieAuth", userPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddMinutes(30)
                });
                return RedirectToAction("PrincipalConsultor", "usuario", new { cita = false });
            }
            return RedirectToAction("Iniciar_Sesion","usuario");
        }

        public IActionResult cerrarSesion()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult PrincipalAdmin(bool agregado = false)
        {
            ViewBag.Agregado = agregado;
            return View ();
        }
        [Authorize(Roles = "Usuario")]
        public IActionResult PrincipalUsuario(bool agregado = false)
        {
            ViewData["Citas"] = new SelectList(_context.Cita.Where(c=>c.Id))
        }
        /* // GET: Usuario/Details/5
         public async Task<IActionResult> Details(int? id)
         {
             if (id == null || _context.Usuario == null)
             {
                 return NotFound();
             }

             var usuario = await _context.Usuario
                 .FirstOrDefaultAsync(m => m.Id_Usuario == id);
             if (usuario == null)
             {
                 return NotFound();
             }

             return View(usuario);
         }

         // GET: Usuario/Create
         public IActionResult Create()
         {
             return View();
         }

         // POST: Usuario/Create
         // To protect from overposting attacks, enable the specific properties you want to bind to.
         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create([Bind("Id_Usuario,Nombre_Usuario,Cedula,Correo,Telefono,Telefono_Fijo,contraseña,Id_Rol")] Usuario usuario)
         {
             if (ModelState.IsValid)
             {
                 _context.Add(usuario);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
             return View(usuario);
         }

         // GET: Usuario/Edit/5
         public async Task<IActionResult> Edit(int? id)
         {
             if (id == null || _context.Usuario == null)
             {
                 return NotFound();
             }

             var usuario = await _context.Usuario.FindAsync(id);
             if (usuario == null)
             {
                 return NotFound();
             }
             return View(usuario);
         }

         // POST: Usuario/Edit/5
         // To protect from overposting attacks, enable the specific properties you want to bind to.
         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(int id, [Bind("Id_Usuario,Nombre_Usuario,Cedula,Correo,Telefono,Telefono_Fijo,contraseña,Id_Rol")] Usuario usuario)
         {
             if (id != usuario.Id_Usuario)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 try
                 {
                     _context.Update(usuario);
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!UsuarioExists(usuario.Id_Usuario))
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
             return View(usuario);
         }

         // GET: Usuario/Delete/5
         public async Task<IActionResult> Delete(int? id)
         {
             if (id == null || _context.Usuario == null)
             {
                 return NotFound();
             }

             var usuario = await _context.Usuario
                 .FirstOrDefaultAsync(m => m.Id_Usuario == id);
             if (usuario == null)
             {
                 return NotFound();
             }

             return View(usuario);
         }

         // POST: Usuario/Delete/5
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> DeleteConfirmed(int id)
         {
             if (_context.Usuario == null)
             {
                 return Problem("Entity set 'CitaFacilContext.Usuario'  is null.");
             }
             var usuario = await _context.Usuario.FindAsync(id);
             if (usuario != null)
             {
                 _context.Usuario.Remove(usuario);
             }

             await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
         }

         private bool UsuarioExists(int id)
         {
           return (_context.Usuario?.Any(e => e.Id_Usuario == id)).GetValueOrDefault();
         }*/
    }
}
