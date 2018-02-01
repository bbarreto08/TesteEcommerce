using Newtonsoft.Json;
using PagedList;
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
    public class PedidoController : Controller
    {
        Uri url = new Uri("http://localhost:61472");

        // GET: Pedido
        public ActionResult Index(int? pagina)
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Index", "Login");
            }

            List<DadosPedidos> listDadosPedidos = new List<DadosPedidos>();

            HttpClient client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.Identity.Name);

            HttpResponseMessage response = client.GetAsync("/api/Pedido").Result;

            if (response.IsSuccessStatusCode)
            {
                listDadosPedidos = JsonConvert.DeserializeObject<List<DadosPedidos>>(response.Content.ReadAsStringAsync().Result);
            }

            if (listDadosPedidos.Count() <= 0)
            {
                ViewBag.errorMessage = "Nenhum pedido encontrado";
                PagedList<DadosPedidos> pdAux = new PagedList<DadosPedidos>(null, 1, 1);

                return View(pdAux);
            }

            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            PagedList<DadosPedidos> pd = new PagedList<DadosPedidos>(listDadosPedidos, numeroPagina, tamanhoPagina);

            return View(pd);
        }

        public ActionResult Detalhes(int carrinho_id, int? pagina)
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Index", "Login");
            }
            // Criar servico retornar detalhes chamada get igual ontem
            List<DetalhesPedido> listDetalhesPedido = new List<DetalhesPedido>();

            HttpClient client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.Identity.Name);

            HttpResponseMessage response = client.GetAsync("/api/Pedido?carrinho_id=" + carrinho_id).Result;

            if (response.IsSuccessStatusCode)
            {
                listDetalhesPedido = JsonConvert.DeserializeObject<List<DetalhesPedido>>(response.Content.ReadAsStringAsync().Result);
            }

            if (listDetalhesPedido.Count() <= 0)
            {
                ViewBag.errorMessage = "Nenhum item do pedido encontrado";
                PagedList<DetalhesPedido> pdAux = new PagedList<DetalhesPedido>(null, 1, 1);

                return View(pdAux);
            }

            @ViewBag.Total = listDetalhesPedido.Sum(x => (x.carrinhoItens_valorTotalItem));

            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            PagedList<DetalhesPedido> pd = new PagedList<DetalhesPedido>(listDetalhesPedido, numeroPagina, tamanhoPagina);

            return View(pd);
        }
    }
}