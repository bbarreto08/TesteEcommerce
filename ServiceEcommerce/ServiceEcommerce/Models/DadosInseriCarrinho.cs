using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceEcommerce.Models
{
    public class DadosInseriCarrinho
    {
        public carrinho carrinho { get; set; }
        public carrinhoItens carrinhoItens { get; set; }
    }
}