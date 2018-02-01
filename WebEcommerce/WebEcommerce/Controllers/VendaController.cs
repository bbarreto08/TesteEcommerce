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
    public class VendaController : Controller
    {
        Uri url = new Uri("http://localhost:61472");
        // GET: Venda
        public ActionResult Index(int cliente_id, int? pagina)
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Index", "Login");
            }

            List<Produto> listProduto = new List<Produto>();
            Carrinho carrinho;
            HttpClient client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.Identity.Name);

            HttpResponseMessage response = client.GetAsync("/api/Produtos").Result;

            if (response.IsSuccessStatusCode)
            {
                listProduto = JsonConvert.DeserializeObject<List<Produto>>(response.Content.ReadAsStringAsync().Result);
            }

            if (listProduto.Count() <= 0)
            {
                ViewBag.errorMessage = "Nenhum produto cadastrado";
                PagedList<Produto> pdAux = new PagedList<Produto>(null, 1, 1);

                return View(pdAux);                
            }

            if(Session["carrinho"] == null)
            {
                carrinho = criarCarrinho(cliente_id);
            } else
            {
                carrinho = (Carrinho) Session["carrinho"];
            }
            
            ViewBag.Carrinho = carrinho.carrinho_id;

            if(Session["Total"] == null)
            {
                ViewBag.Total = carrinho.carrinho_total.ToString();
            }
            else
            {
                ViewBag.Total = Session["Total"].ToString();
            }            

            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            PagedList<Produto> pd = new PagedList<Produto>(listProduto, numeroPagina, tamanhoPagina);

            return View(pd);
        }

        public ActionResult Finalizar(int? pagina)
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Index", "Login");
            }

            Carrinho carrinho;
            List<DadosCarrinho> listDadosCarrinho;

            if (Session["carrinho"] != null)
            {
                carrinho = (Carrinho)Session["carrinho"];

                HttpClient client = new HttpClient();
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.Identity.Name);

                HttpResponseMessage response = client.GetAsync("/api/DadosCarrinho?idCarrinho=" + carrinho.carrinho_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    listDadosCarrinho = JsonConvert.DeserializeObject<List<DadosCarrinho>>(response.Content.ReadAsStringAsync().Result);

                    int tamanhoPagina = 10;
                    int numeroPagina = pagina ?? 1;
                    PagedList<DadosCarrinho> pd = new PagedList<DadosCarrinho>(listDadosCarrinho, numeroPagina, tamanhoPagina);

                    return View(pd);
                }

            }
                        
            return View();
            
        }
        
        public ActionResult FinalizarPedido(int idCarrinho)
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Index", "Login");
            }
            // criar pedido
            HttpClient client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.Identity.Name);

            HttpResponseMessage response = client.PostAsJsonAsync("/api/Pedido?idCarrinho=" + idCarrinho, "").Result;

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.errorMessage = "O pedido nao foi criado";
            }

            return RedirectToAction("Index", "Home");
        }

        private Carrinho criarCarrinho(int cliente_id)
        {
            Carrinho carrinho = new Carrinho();
            HttpClient client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.Identity.Name);

            client.DefaultRequestHeaders.Add("idCliente", cliente_id.ToString());

            HttpResponseMessage response = client.PostAsJsonAsync("/api/Carrinho?idCliente=" + cliente_id, "").Result;

            if (response.IsSuccessStatusCode)
            {
                carrinho = JsonConvert.DeserializeObject<Carrinho>(response.Content.ReadAsStringAsync().Result);
            }

            return carrinho;
        }

    }
}