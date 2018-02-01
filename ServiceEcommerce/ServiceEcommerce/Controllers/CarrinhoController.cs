using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ServiceEcommerce.Models;

namespace ServiceEcommerce.Controllers
{        
    /// <summary>
    /// Recurso relacionado a operacoes de Carrinho
    /// </summary>
    [Authorize] 
    public class CarrinhoController : ApiController
    {
        ecommerceTesteEntities bdContext = new ecommerceTesteEntities();
        /// <summary>
        /// Criar carrinho para o cliente
        /// </summary>
        /// <param name="idCliente">Codigo do cliente</param>
        /// <returns></returns>     
        public HttpResponseMessage Post(int idCliente)
        {
            DefaultReturn DefaultReturn = new DefaultReturn();

            carrinho carrinho = new carrinho()
            {
                carrinho_dataCadastro = DateTime.Now,
                cliente_id = idCliente
            };

            try
            {
                bdContext.carrinho.Add(carrinho);
                bdContext.SaveChanges();
            }
            catch (Exception)
            {
                DefaultReturn.code = (int)HttpStatusCode.InternalServerError;
                DefaultReturn.messageError = "Falha ao criar o carrinho.";

                return Request.CreateResponse<DefaultReturn>(HttpStatusCode.InternalServerError, DefaultReturn);
            }

            return Request.CreateResponse<carrinho>(HttpStatusCode.Created, carrinho);
        }

        /// <summary>
        /// Retornar carrinho
        /// </summary>
        /// <param name="idCarrinho"></param>
        /// <returns>Dados do carrinho criado</returns>
        public HttpResponseMessage Get(int idCarrinho)
        {
            DefaultReturn DefaultReturn = new DefaultReturn();

            List<carrinho> carrinho = ( from bd in bdContext.carrinho
                                        where bd.carrinho_id.Equals(idCarrinho)
                                        select bd).ToList();

            if(carrinho.Count() <= 0)
            {
                DefaultReturn.code = (int)HttpStatusCode.Forbidden;
                DefaultReturn.messageError = "Carrinho nao identificado.";

                return Request.CreateResponse<DefaultReturn>(HttpStatusCode.InternalServerError, DefaultReturn);
            }                                           

            return Request.CreateResponse<carrinho>(HttpStatusCode.OK, carrinho[0]);
        }
    }
}
