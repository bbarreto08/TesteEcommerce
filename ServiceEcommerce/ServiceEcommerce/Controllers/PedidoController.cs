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
    public class PedidoController : ApiController
    {
        ecommerceTesteEntities bdContext = new ecommerceTesteEntities();

        /// <summary>
        /// Retorna a lista de todos os pedidos
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {
            DefaultReturn DefaultReturn = new DefaultReturn();

            List<DadosPedidos> listDadosPedidos = (from dados in bdContext.prcTodosPedidos()
                                                     select new DadosPedidos()
                                                     {
                                                         carrinho_id = dados.carrinho_id,
                                                         cliente_id = dados.cliente_id,
                                                         cliente_nome = dados.cliente_nome,
                                                         pedido_dataCadastro = dados.pedido_dataCadastro,
                                                         pedido_valor = dados.pedido_valor
                                                     }).ToList();

            if (listDadosPedidos.Count() <= 0)
            {
                DefaultReturn.code = (int)HttpStatusCode.Forbidden;
                DefaultReturn.messageError = "Nenhum pedido localizado.";

                return Request.CreateResponse<DefaultReturn>(HttpStatusCode.InternalServerError, DefaultReturn);
            }

            return Request.CreateResponse<List<DadosPedidos>>(HttpStatusCode.OK, listDadosPedidos);
        }

        /// <summary>
        /// Retorna detalhes de um pedido
        /// </summary>
        /// <param name="carrinho_id"></param>
        /// <returns></returns>
        public HttpResponseMessage GetDetalhes(int carrinho_id)
        {
            DefaultReturn DefaultReturn = new DefaultReturn();

            List<DetalhesPedido> listDetalhesPedido = (from dados in bdContext.prcDetalhesPedido(carrinho_id)
                                                   select new DetalhesPedido()
                                                   {
                                                       carrinhoItens_dataCadastro = dados.carrinhoItens_dataCadastro,
                                                       carrinhoItens_quantidade = dados.carrinhoItens_quantidade,
                                                       carrinhoItens_valorTotalItem = dados.carrinhoItens_valorTotalItem,
                                                       carrinhoItens_valorUnitario = dados.carrinhoItens_valorUnitario,
                                                       produto_desc = dados.produto_desc,
                                                       produto_nome = dados.produto_nome
                                                   }).ToList();

            if (listDetalhesPedido.Count() <= 0)
            {
                DefaultReturn.code = (int)HttpStatusCode.Forbidden;
                DefaultReturn.messageError = "Falha ao buscar os detalhes localizado.";

                return Request.CreateResponse<DefaultReturn>(HttpStatusCode.InternalServerError, DefaultReturn);
            }

            return Request.CreateResponse<List<DetalhesPedido>>(HttpStatusCode.OK, listDetalhesPedido);
        }

        /// <summary>
        /// Confirma o pedido 
        /// </summary>
        /// <param name="idCarrinho"></param>
        /// <returns></returns>
        public HttpResponseMessage PostInserirPedido(int idCarrinho)
        {
            DefaultReturn DefaultReturn = new DefaultReturn();

            try
            {
                bdContext.prcGravarPedido(idCarrinho);
                bdContext.SaveChanges();
            }
            catch (Exception)
            {
                DefaultReturn.code = (int)HttpStatusCode.InternalServerError;
                DefaultReturn.messageError = "Falha ao criar o pedido.";

                return Request.CreateResponse<DefaultReturn>(HttpStatusCode.InternalServerError, DefaultReturn);
            }

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
