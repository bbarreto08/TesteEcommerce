using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WebEcommerce.Models;
using Newtonsoft.Json;
using PagedList;

namespace WebEcommerce.Controllers
{
    public class HomeController : Controller
    {
        Uri url = new Uri("http://localhost:61472");
        public ActionResult Index(int? pagina)
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Index", "Login");
            }

            Session.Clear();
            List<Cliente> listCliente = new List<Cliente>();

            HttpClient client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.Identity.Name);            

            HttpResponseMessage response = client.GetAsync("/api/Cliente").Result;                                    

            if (response.IsSuccessStatusCode)
            {                
               listCliente = JsonConvert.DeserializeObject<List<Cliente>>(response.Content.ReadAsStringAsync().Result);
            }

            if (listCliente.Count() <= 0)
            {
                ViewBag.errorMessage = "Nenhum cliente cadastrado";
                PagedList<Cliente> pdAux = new PagedList<Cliente>(null, 1, 1);

                return View(pdAux);
            }         

            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            PagedList<Cliente> pd = new PagedList<Cliente>(listCliente, numeroPagina, tamanhoPagina);

            return View(pd);
        }     
    }
}