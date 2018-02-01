using Newtonsoft.Json;
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
    public class CarrinhoController : Controller
    {
        Uri url = new Uri("http://localhost:61472");

        // GET: Carrinho
        public ActionResult Index(int produto_id, int carrinhoId)
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Index", "Login");
            }

            Carrinho carrinho = getCarrinho(carrinhoId);
            Produto produto = getProduto(produto_id);
            
            CarrinhoItens carrinhoItens = new CarrinhoItens()
            {
                carrinhoItens_carrinho_id = carrinhoId,
                carrinhoItens_dataCadastro = DateTime.Now,
                carrinhoItens_produto_id    = produto_id,
                carrinhoItens_valorUnitario = produto.produto_preco                
            };
            
            DadosInserirCarrinho dadosInserirCarrinho = new DadosInserirCarrinho(carrinho, carrinhoItens);

            Session["DadosInserirCarrinho"] = dadosInserirCarrinho;

            return View(produto);
        }

        [HttpPost]
        public ActionResult Index(int produto_id, int? quantidade)
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Index", "Login");
            }

            Produto produto = getProduto(produto_id);
            DadosInserirCarrinho DadosInserirCarrinho = (DadosInserirCarrinho) Session["DadosInserirCarrinho"];
            DadosInserirCarrinho.carrinhoItens.carrinhoItens_quantidade = quantidade;
            DadosInserirCarrinho.carrinhoItens.carrinhoItens_valorTotalItem = DadosInserirCarrinho.carrinhoItens.carrinhoItens_valorUnitario * quantidade;
            DadosInserirCarrinho.carrinho.carrinho_total = Convert.ToDecimal(DadosInserirCarrinho.carrinho.carrinho_total) + Convert.ToDecimal(DadosInserirCarrinho.carrinhoItens.carrinhoItens_valorTotalItem);
            
            HttpClient client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.Identity.Name);

            if(quantidade == null)
            {
                ViewBag.errorMessage = "Deve ser informado uma quantidade valida!";
                Session["carrinho"] = DadosInserirCarrinho.carrinho;
                Session["Total"] = Convert.ToDecimal(DadosInserirCarrinho.carrinho.carrinho_total);

                return View(produto);
            }

            HttpResponseMessage response = client.PostAsJsonAsync("/api/CarrinhoItens", DadosInserirCarrinho).Result;

            if (response.IsSuccessStatusCode)
            {
                Session["carrinho"] = DadosInserirCarrinho.carrinho;
                Session["Total"] = Convert.ToDecimal(DadosInserirCarrinho.carrinho.carrinho_total);
                return RedirectToAction("Index", "Venda", new { cliente_id = DadosInserirCarrinho.carrinho.cliente_id });
            }

            return RedirectToAction("Index", "Home");
        }

        private Carrinho getCarrinho(int carrinhoId)
        {
            Carrinho carrinho = null;
            HttpClient client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.Identity.Name);

            HttpResponseMessage response = client.GetAsync("/api/Carrinho?idCarrinho=" + carrinhoId).Result;

            if (response.IsSuccessStatusCode)
            {
                carrinho = JsonConvert.DeserializeObject<Carrinho>(response.Content.ReadAsStringAsync().Result);
            }

            return carrinho;
        }

        private Produto getProduto(int produtoId)
        {
            Produto produto = null;
            HttpClient client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.Identity.Name);

            HttpResponseMessage response = client.GetAsync("/api/Produtos?idProduto=" + produtoId).Result;

            if (response.IsSuccessStatusCode)
            {
                produto = JsonConvert.DeserializeObject<Produto>(response.Content.ReadAsStringAsync().Result);
            }

            return produto;
        }
    }
}