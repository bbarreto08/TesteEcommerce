using ServiceEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiceEcommerce.Controllers
{
    [Authorize()]
    public class CategoriaController : ApiController
    {
        ecommerceTesteEntities bdContext = new ecommerceTesteEntities();
        /// <summary>
        /// Cadastrar nova categoria
        /// </summary>
        /// <param name="categoria">Objeto categoria</param>
        /// <returns></returns>
        public HttpResponseMessage Post(categoria categoria)
        {
            try
            {
                bdContext.categoria.Add(categoria);
                bdContext.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
