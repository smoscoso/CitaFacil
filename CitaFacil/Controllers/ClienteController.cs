using CitaFacil.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using CitaFacil.Models;


namespace CitaFacil.Controllers
{
    public class ClienteController : Controller
    {
        private readonly CitaFacilContext _contexto;

        public ClienteController(CitaFacilContext dbC)
        {
           _contexto= dbC;
        }
  




        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Historial()
          {
            return View(await _contexto.Fecha.ToListAsync());
        }
        [HttpGet]
        public IActionResult Calendario()
        {
            return View();
        }
     
        [HttpPost]

        public async Task<IActionResult> Calendario([FromBody] Fecha fecha)
        {
            if (ModelState.IsValid)
            {
                _contexto.Fecha.Add(fecha);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Historial));
            }
            return View();
        }



        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}