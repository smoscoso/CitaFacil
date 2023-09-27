using Microsoft.AspNetCore.Mvc;
using CitaFacil.Models;
using CitaFacil.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace CitaFacil.Controllers
{
    public class InicioController : Controller
    {
        private readonly IUsuarioService _usuarioServico;
        public InicioController(IUsuarioService usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }
        public IActionResult RegistrarseCliente ()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> RegistrarseCliente(Registrar_Cliente modeloC)
        {
            modeloC.passsword= ServiciosCitaFacil.CifrarContraseña(modeloC.passsword);
            Registrar_Cliente clienteCreado = await _usuarioServico.SaveRegistrar_Cliente(modeloC);
            if (clienteCreado.Correo != null)
            {
                return RedirectToAction ("IniciarSesionCliente", "Inicio");
            }
            ViewData["Message"] = "No se pudo Crear el cliente";
            return View();
        }
        public IActionResult IniciarSesionCliente()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> IniciarSesionCliente(string correo, string clave)
        {   Registrar_Cliente clienteEncontrado = await _usuarioServico.GetRegistrar_Cliente(correo, ServiciosCitaFacil.CifrarContraseña(clave));
            if(clienteEncontrado != null)
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
            return RedirectToAction("Index","Home");
        }

    }
}
