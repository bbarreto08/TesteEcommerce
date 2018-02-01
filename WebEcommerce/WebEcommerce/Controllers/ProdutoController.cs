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
    public class ProdutoController : Controller
    {
        Uri url = new Uri("http://localhost:61472");

        // GET: Produto
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(Produto produto)
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Index", "Login");
            }

            bool auxErro = false;

            if (string.IsNullOrEmpty(produto.produto_nome))
            {
                auxErro = true;
                @ViewBag.errorMessage = "Informe o nome do produto";
            }

            if (string.IsNullOrEmpty(produto.produto_desc))
            {
                auxErro = true;
                @ViewBag.errorMessage = "Informe uma descricao";
            }

            if (string.IsNullOrEmpty(produto.produto_preco.ToString()))
            {
                auxErro = true;
                @ViewBag.errorMessage = "Informe o valor do produto";
            }

            if (auxErro)
            {
                return View();
            }

            HttpClient client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.Identity.Name);

            HttpResponseMessage response = client.PostAsJsonAsync("/api/Produtos", produto).Result;

            if (response.IsSuccessStatusCode)
            {
                @ViewBag.Message = "Produto inserido com successo!";
                return View();
            } else
            {
                @ViewBag.errorMessage = "Falha ao cadastrar o produto";
            }

            return View();

        }
    }
}