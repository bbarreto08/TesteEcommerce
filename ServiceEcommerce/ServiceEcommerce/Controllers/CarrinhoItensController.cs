using ServiceEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiceEcommerce.Controllers
{
    public class CarrinhoItensController : ApiController
    {
        ecommerceTesteEntities bdContext = new ecommerceTesteEntities();

        /// <summary>
        /// Inserir item no carrinho
        /// </summary>
        /// <param name="DadosInseriCarrinho"></param>
        /// <returns></returns>
        [Authorize()]
        public HttpResponseMessage PostInserirCarrinho(DadosInseriCarrinho DadosInseriCarrinho)
        {
            DefaultReturn DefaultReturn = new DefaultReturn();
            
            try
            {
                bdContext.carrinhoItens.Add(DadosInseriCarrinho.carrinhoItens);

                carrinho carrinho = bdContext.carrinho.Single(tb => tb.carrinho_id.Equals(DadosInseriCarrinho.carrinho.carrinho_id));
                carrinho.carrinho_total = Convert.ToDecimal(DadosInseriCarrinho.carrinho.carrinho_total);

                bdContext.SaveChanges();
            }
            catch (Exception)
            {
                DefaultReturn.code = (int)HttpStatusCode.InternalServerError;
                DefaultReturn.messageError = "Falha ao inserir item no carrinho.";

                return Request.CreateResponse<DefaultReturn>(HttpStatusCode.InternalServerError, DefaultReturn);
            }

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
