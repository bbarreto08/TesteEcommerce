using ServiceEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiceEcommerce.Controllers
{
    public class LoginController : ApiController
    {
        ecommerceTesteEntities bdContext = new ecommerceTesteEntities();

        /// <summary>
        /// Autenticar usuario
        /// </summary>
        /// <param name="cadastroUsuario"></param>
        /// <returns></returns>
        [Authorize()]
        public HttpResponseMessage PostCadastrarUsuario(cadastroUsuario cadastroUsuario)
        {
            DefaultReturn DefaultReturn = new DefaultReturn();

            try
            {
                bdContext.cadastroUsuario.Add(cadastroUsuario);               
                bdContext.SaveChanges();
            }
            catch (Exception)
            {
                DefaultReturn.code = (int)HttpStatusCode.InternalServerError;
                DefaultReturn.messageError = "Falha ao cadastrar novo usuario.";

                return Request.CreateResponse<DefaultReturn>(HttpStatusCode.InternalServerError, DefaultReturn);
            }

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
