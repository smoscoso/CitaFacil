using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CitaFacil.Data;
using CitaFacil.Models;
using CitaFacil.Models.DTO;
using Newtonsoft.Json;
using CitaFacil.Services;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CitaFacil.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly CitaFacilContext _context;
        private readonly ServiciosCitaFacil _servicioCF;

        public UsuarioController(CitaFacilContext context, IEmailService emailService)
        {
            this._context = context;
            this._servicioCF = new ServiciosCitaFacil(_context,emailService);
        }

        [Authorize(Roles = "Usuario")]
        public IActionResult Index()
        {
            int estado = int.Parse(User.FindFirst("Estado").Value);
            if(estado == 4 || estado == 6)
            {
                return RedirectToAction("EnRevision");
            }
            else
            {
                return RedirectToAction("Index","Cliente");
            }
                
            return View();
        }

        public IActionResult EnRevision() { 
            return View();
        }

        public async Task<IActionResult> RegistroUsuario (UsuarioDTO usuario, string error)
        {
            ViewBag.error = error;
            if (TempData["usuario"] != null)
            {
                usuario = JsonConvert.DeserializeObject<UsuarioDTO>(TempData["usuario"].ToString());
            }
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarUsuario(UsuarioDTO usuario)
        {
            string error = _servicioCF.ValidarUsuario(usuario);
            if (error == null)
            {
                if (_servicioCF.AgregarCliente(usuario) >= 1)
                {
                    _servicioCF.EnviarCorreo(usuario.correo);
                    return RedirectToAction("IniciarSesion", "Home");
                }

            }
            TempData["usuario"] = JsonConvert.SerializeObject(usuario);
            return RedirectToAction("RegistroUsuario", new { error = error });
        }
    }
}
