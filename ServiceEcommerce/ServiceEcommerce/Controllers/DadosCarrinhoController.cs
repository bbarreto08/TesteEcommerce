using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServiceEcommerce.Models;

namespace ServiceEcommerce.Controllers
{
    [Authorize()]
    public class DadosCarrinhoController : ApiController
    {
        ecommerceTesteEntities bdContext = new ecommerceTesteEntities();
        /// <summary>
        /// Retorna lista dos itens de determinado carrinho
        /// </summary>
        /// <param name="idCarrinho">Detalhes do carrinho</param>
        /// <returns></returns>
        public HttpResponseMessage Get(int idCarrinho)
        {
            DefaultReturn DefaultReturn = new DefaultReturn();

            List<DadosCarrinho> listDadosCarrinho = (from dados in bdContext.prcdadosCarrinho(idCarrinho)
                                                     select new DadosCarrinho() { carrinho_id = dados.carrinho_id,
                                                                                  produto_nome = dados.produto_nome,
                                                                                  produto_desc = dados.produto_desc,
                                                                                  carrinhoItens_valorUnitario = dados.carrinhoItens_valorUnitario,
                                                                                  carrinhoItens_valorTotalItem = dados.carrinhoItens_valorTotalItem}).ToList();

            if (listDadosCarrinho.Count() <= 0)
            {
                DefaultReturn.code = (int)HttpStatusCode.Forbidden;
                DefaultReturn.messageError = "Carrinho nao identificado.";

                return Request.CreateResponse<DefaultReturn>(HttpStatusCode.InternalServerError, DefaultReturn);
            }

            return Request.CreateResponse<List<DadosCarrinho>>(HttpStatusCode.OK, listDadosCarrinho);
        }
    }
}
