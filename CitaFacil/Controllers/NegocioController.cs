using CitaFacil.Data;
using CitaFacil.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitaFacil.Controllers
{
    public class NegocioController : Controller
    {
        private readonly CitaFacilContext _contexto;
        public NegocioController(CitaFacilContext dbC)
        {
            _contexto = dbC;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dash()
        {
            return View();
        }

        public IActionResult CalendarioN()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CalendarioN([FromBody] Fecha fecha)
        {
            return Json(new { result = "Redirect", url = Url.Action("DashCitas", "Dashboard"), data = fecha.fecha });
        }

    }
}
