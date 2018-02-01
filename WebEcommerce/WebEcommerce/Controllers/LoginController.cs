using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebEcommerce.Models;

namespace WebEcommerce.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login login)
        {
            RestClient client = new RestClient("http://localhost:61472");

            RestRequest request = new RestRequest("/api/security/token", Method.POST);

            request.AddParameter("grant_type", "password");
            request.AddParameter("username", login.nameUser);
            request.AddParameter("password", login.password);

            IRestResponse<TokenResponse> response = client.Execute<TokenResponse>(request);
            string token = response.Data.access_token;

            if (!String.IsNullOrEmpty(token))
            {
                FormsAuthentication.SetAuthCookie(token, false);

                return RedirectToAction("Index", "Home");
            } else
            {
                ViewBag.errorMessage = "Falha ao autenticar o usuario.";
                return View();
            }            
        }
    }
}