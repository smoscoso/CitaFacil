using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;    //Json to c#
using System.Threading.Tasks; //solicitudes asincronas
using CitaFacil.Models.Paypal_Order;
using JsonRequest = System.Web.Mvc;
using CitaFacil.Models.Paypal_Transaction;



namespace CitaFacil.Controllers
{
    public class ServicioPagoController : Controller
    {
        //controlador para la vista de pago
        public IActionResult Planes()
        {
            return View();
        }
        public async Task<ActionResult> About()
        {

            //id de la autorizacion para obtener el dinero
            //string token = Request.QueryString["token"];     no compatible con core
            string token = HttpContext.Request.Query["token"];

            bool status = false;


            using (var client = new HttpClient())
            {

                // -> CLIENT ID - SECRET
                var userName = "AUPoPq_vkjUFESIHabAMgl_QBYMaBHeupTF34PKebRpGMcSo3JRb-dWauSArSbkU7JFTkYqEz3K1iNbN";
                var passwd = "ENFfdUewSc8RcerI2z1HCPfF0VYtwNg_Ai3LvrJK0UEkVec8Hdd2U-NjfYEid3fdTAYoahwIUxqvSB0l";

                client.BaseAddress = new Uri("https://api-m.sandbox.paypal.com");

                var authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                var data = new StringContent("{}", Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"/v2/checkout/orders/{token}/capture", data);


                status = response.IsSuccessStatusCode;

                ViewData["Status"] = status;
                if (status)
                {
                    var jsonRespuesta = response.Content.ReadAsStringAsync().Result;

                    PaypalTransaction objeto = JsonConvert.DeserializeObject<PaypalTransaction>(jsonRespuesta);

                    ViewData["IdTransaccion"] = objeto.purchase_units[0].payments.captures[0].id;
                }

            }


            return View();
        }
        // GET: ServicioPagoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ServicioPagoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ServicioPagoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServicioPagoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ServicioPagoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ServicioPagoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ServicioPagoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ServicioPagoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        /* peticion de red por tarea asincrona  */
        [HttpPost]
        //Ipublic JsonResult Paypal(string precio) ---> EDITAR POR LA LINEA DE ABAJO
        public async Task<JsonResult> Paypal(string precio, string producto)
        {
            bool status = false;
            string respuesta = string.Empty;
            //peticion
            using (var client = new HttpClient())
            {
                var userName = "AUPoPq_vkjUFESIHabAMgl_QBYMaBHeupTF34PKebRpGMcSo3JRb-dWauSArSbkU7JFTkYqEz3K1iNbN";
                var passwd = "ENFfdUewSc8RcerI2z1HCPfF0VYtwNg_Ai3LvrJK0UEkVec8Hdd2U-NjfYEid3fdTAYoahwIUxqvSB0l";
                client.BaseAddress = new Uri("https://api-m.sandbox.paypal.com");
                //tomar login como autorizacion y convertir en binario
                var authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                //
                var orden = new PaypalOrder()
                {
                    intent = "CAPTURE",
                    purchase_units = new List<Models.Paypal_Order.PurchaseUnit>()
                    {
                        new Models.Paypal_Order.PurchaseUnit()
                        {
                            amount = new Models.Paypal_Order.Amount()
                            {
                                currency_code = "USD",
                                value = precio
                            },
                            description = producto
                        }
                    },
                    application_context = new ApplicationContext()
                    {
                        brand_name = "Mi Tienda",
                        landing_page = "NO_PREFERENCE",
                        user_action = "PAY_NOW", //Accion para que paypal muestre el monto de pago
                        return_url = "/",// cuando se aprovo la solicitud del cobro
                        cancel_url = "/ServicioPago/Planes"// cuando cancela la operacion
                    }
                };
                //convertir clases en tipo Json
                var json = JsonConvert.SerializeObject(orden);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                //use of API
                HttpResponseMessage response = await client.PostAsync("/v2/checkout/orders", data);

                status = response.IsSuccessStatusCode;


                if (status)
                {
                    respuesta = response.Content.ReadAsStringAsync().Result;
                }
            }

            return Json(new { status = status, respuesta = respuesta }, JsonRequest.JsonRequestBehavior.AllowGet);
        }
    }
}