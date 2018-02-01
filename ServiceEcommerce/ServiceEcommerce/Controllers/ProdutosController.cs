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
    public class ProdutosController : ApiController
    {
        ecommerceTesteEntities bdContext = new ecommerceTesteEntities();

        /// <summary>
        /// Cadastrar um novo produto
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(produto produto)
        {
            try
            {
                bdContext.produto.Add(produto);
                bdContext.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Metodo para obeter a lista de todos os produtos disponiveis ativos
        /// </summary>
        /// <returns></returns>        
        public List<produto> Get()
        {
            List<produto> listProdutos;

            try
            {
                listProdutos = (from bd in bdContext.produto
                               where bd.produto_ativo == true
                               select bd).ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return listProdutos;
        }

        /// <summary>
        /// Retorna dados de um determinado produto
        /// </summary>
        /// <param name="idProduto"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(int idProduto)
        {
            DefaultReturn DefaultReturn = new DefaultReturn();

            List<produto> produto = (   from bd in bdContext.produto
                                        where bd.produto_id.Equals(idProduto)
                                        select bd).ToList();

            if (produto.Count() <= 0)
            {
                DefaultReturn.code = (int)HttpStatusCode.Forbidden;
                DefaultReturn.messageError = "Produto nao identificado.";

                return Request.CreateResponse<DefaultReturn>(HttpStatusCode.InternalServerError, DefaultReturn);
            }

            return Request.CreateResponse<produto>(HttpStatusCode.OK, produto[0]);
        }
    }
}

