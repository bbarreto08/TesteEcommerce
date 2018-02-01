using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServiceEcommerce.Models;

namespace ServiceEcommerce.Controllers
{
    public class ClienteController : ApiController
    {
        ecommerceTesteEntities bdContext = new ecommerceTesteEntities();
        
        /// <summary>
        /// Metodo realiza a insercao de um novo cliente, necessita o uso do token de 
        /// autenticacao
        /// </summary>
        /// <param name="cliente">Objeto com dados do cliente</param>
        /// <returns></returns>
        [Authorize()]
        public HttpResponseMessage Post(cliente cliente)
        {
            DefaultReturn DefaultReturn = new DefaultReturn();

            if (cliente.cliente_nome.Equals(""))
            {
                DefaultReturn.code = (int) HttpStatusCode.Forbidden;
                DefaultReturn.messageError = "Nome nao informado.";
            }

            if (cliente.cliente_email.Equals(""))
            {
                DefaultReturn.code = (int)HttpStatusCode.Forbidden;
                DefaultReturn.messageError = DefaultReturn.messageError + "/nEmail nao informado.";
            }

            if (DefaultReturn.code == 0)
            {
                bdContext.cliente.Add(cliente);
                bdContext.SaveChanges();

                return Request.CreateResponse<DefaultReturn>(HttpStatusCode.OK, DefaultReturn);
            } else
            {
                return Request.CreateResponse<DefaultReturn>(HttpStatusCode.Forbidden, DefaultReturn);
            }            
        }

        /// <summary>
        /// Recupera todos os clientes cadastrados
        /// necessita o uso do token de autenticacao
        /// </summary>
        /// <returns>Lista de clientes</returns>
        [Authorize()]
        public List<cliente> Get()
        {
            List<cliente> listClientes;

            try
            {
                listClientes = (from bd in bdContext.cliente
                               select bd).ToList()
                               ;
            }
            catch (Exception)
            {

                throw;
            }
            
            return listClientes;
        }
    }
}
