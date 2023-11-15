using CitaFacil.Data;
using CitaFacil.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitaFacil.Controllers
{
    public class DashboardController : Controller
    {
        private readonly CitaFacilContext _contexto;
        public DashboardController(CitaFacilContext dbC)
        {
            _contexto = dbC;
        }
        public IActionResult DashPerfil()
        {

            // Obtiene datos desde la base de datos
            var empresas = _contexto.Empresa.ToList();

            return View(empresas);


        }
        public IActionResult DashCitas(string fecha)
        {
            Fecha newFecha = new Fecha();
            newFecha.fecha = Convert.ToDateTime(fecha);
            return View(newFecha);
        }
        
        //[HttpPost]
        //public IActionResult DashCitas([FromBody] Fecha fecha)
        //{
        //    var t = fecha;
        //    return View();
        //}

        public IActionResult DashServicios()
        {
            var servicios = _contexto.ServiciosN.ToList();
            return View(servicios);
        }




        [HttpGet]
    public IActionResult DashCaracter()
    {
        return View();
    }

    [HttpPost]
    public IActionResult DashCaracter(ServiciosNegocio servicio)
    {
        if (ModelState.IsValid)
        {
            _contexto.ServiciosN.Add(servicio);
            _contexto.SaveChanges();
            return RedirectToAction("DashServicios");
        }

        return View(servicio);
    }


    }
}
