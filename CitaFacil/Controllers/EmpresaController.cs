using CitaFacil.Data;
using CitaFacil.Models;
using CitaFacil.Models.DTO;
using CitaFacil.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CitaFacil.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly CitaFacilContext _context;
        private readonly ServiciosCitaFacil _servicioCF;

        public EmpresaController(CitaFacilContext context, IEmailService emailService)
        {
            this._context = context;
            this._servicioCF = new ServiciosCitaFacil(_context, emailService);
        }

        [Authorize(Roles = "Empresa,Administrador")]
        public IActionResult Index()
        {
            int estado = int.Parse(User.FindFirst("Estado").Value);
            if (estado == 4 || estado == 6)
            {
                return RedirectToAction("EnRevision");
            }
<<<<<<< HEAD
            else
            {

                return RedirectToAction("Index", "Negocio");
            }
         ///////////////////////////////////////////////////////////////////////
=======
>>>>>>> c56473f1decee8c3559ed27d8d1f1207e063efde
            return View();
        }

        public IActionResult EnRevision()
        {
            return View();
        }

        public async Task<IActionResult> RegistroEmpresa(EmpresaDTO empresa, string error)
        {
            ViewBag.error = error;
            if (TempData["empresa"] != null)
            {
                empresa = JsonConvert.DeserializeObject<EmpresaDTO>(TempData["empresa"].ToString());
            }
            return View(empresa);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarEmpresa(EmpresaDTO empresa)
        {
            string error = _servicioCF.ValidarEmpresa(empresa);
            if (error == null)
            {
                if (_servicioCF.AgregarEmpresa(empresa) >= 1)
                {
                    _servicioCF.EnviarCorreo(empresa.correo);
                    return RedirectToAction("IniciarSesion", "Home");
                }

            }
            TempData["empresa"] = JsonConvert.SerializeObject(empresa);
            return RedirectToAction("RegistroEmpresa", new { error = error });
        }
    }
}
