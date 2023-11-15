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
        /*
        [HttpPost]
        public async Task <IActionResult> RegistrarseCliente(Usuario modeloC)
        {
            modeloC.contraseña= ServiciosCitaFacil.CifrarContraseña(modeloC.contraseña);
            Usuario clienteCreado = await _usuarioServico.SaveRegistrar_Cliente(modeloC);
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
        {   Usuario clienteEncontrado = await _usuarioServico.GetRegistrar_Cliente(correo, ServiciosCitaFacil.CifrarContraseña(clave));
            if(clienteEncontrado != null)
            {
                ViewData["Message"] = "No se encontro en el sistema";
            }
            List<Claim> claims = new List<Claim>() { 
                new Claim(ClaimTypes.Name, clienteEncontrado.Nombre_Rol),
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), properties);
            return RedirectToAction("Index","Home");
        }*/

    }
}
