using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WebEcommerce.Models;

namespace WebEcommerce.Controllers
{
    public class CategoriaController : Controller
    {
        Uri url = new Uri("http://localhost:61472");
        // GET: Categoria
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
        
        [HttpPost]
        public ActionResult Index(Categoria categoria)
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Index", "Login");
            }

            if (string.IsNullOrEmpty(categoria.categoria_nome))
            {
                @ViewBag.errorMessage = "Digite um nome para a categoria";
                return View();
            }

            HttpClient client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.Identity.Name);

            categoria.categoria_dataCadastro = DateTime.Now;
            
            HttpResponseMessage response = client.PostAsJsonAsync("/api/Categoria", categoria).Result;

            if (response.IsSuccessStatusCode)
            {
                @ViewBag.Message = "Categoria cadastrada!";
                return View();
            }
            else
            {
                @ViewBag.errorMessage = "Falha ao cadastrar a categoria";
            }

            return View();

        }
    }
}