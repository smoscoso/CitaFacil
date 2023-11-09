﻿using CitaFacil.Data;
using CitaFacil.Models;
using CitaFacil.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace CitaFacil.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CitaFacilContext _context;
        private readonly ServiciosCitaFacil _servicioCF;
        public HomeController(ILogger<HomeController> logger, CitaFacilContext context, IEmailService emailService)
        {
            _logger = logger;
            this._context = context;
            this._servicioCF = new ServiciosCitaFacil(_context, emailService);
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated) 
            {
                string rol = User.FindFirst(ClaimTypes.Role).Value.ToString();
                if (rol.Equals("Empresa"))
                {
                    return RedirectToAction("Index", "Empresa");
                }
                if (rol.Equals("Usuario"))
                {
                    return RedirectToAction("Index", "Usuario");
                }
                if (rol.Equals("Administrador"))
                {
                    return RedirectToAction("Index", "Administrador");
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Registro()
        {
            return View();
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Iniciar_Sesion(string correo, string contrasena, string recordar)
        {
            string error = _servicioCF.VerificarLogin(correo, contrasena);
            Usuario us = _context.Usuario.Include(u => u.Roles).FirstOrDefault(u => u.Correo == correo);
            if (string.IsNullOrEmpty(error))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, us.Id_Usuario.ToString()),
                    new Claim(ClaimTypes.Role, us.Roles.Nombre_Rol),
                    new Claim("Estado",us.Id_EstadoUsuario.ToString())
                };

                var identity = new ClaimsIdentity(claims, "CitaFacilAutenticacion");
                var principal = new ClaimsPrincipal(identity);
                bool recordarU = false;
                if (!string.IsNullOrEmpty(recordar))
                {
                    recordarU = true;
                }
                var authenticationProperties = new AuthenticationProperties
                {
                    IsPersistent = recordarU // Establece IsPersistent en true para recordar la sesión
                };

                HttpContext.SignInAsync("CitaFacilCookie", principal, authenticationProperties);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", new { error = error });
        }

        public IActionResult Cerrar_Sesion()
        {
            HttpContext.SignOutAsync("CitaFacilCookie");
            return RedirectToAction("Index", "Home");
        }
    }
}