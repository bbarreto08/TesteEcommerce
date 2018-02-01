using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEcommerce.Models
{
    public class CarrinhoItens
    {        
        public Nullable<int> carrinhoItens_carrinho_id { get; set; }
        public Nullable<int> carrinhoItens_produto_id { get; set; }
        public Nullable<decimal> carrinhoItens_valorUnitario { get; set; }
        public Nullable<decimal> carrinhoItens_valorTotalItem { get; set; }
        public Nullable<int> carrinhoItens_quantidade { get; set; }
        public Nullable<System.DateTime> carrinhoItens_dataCadastro { get; set; }
    }
}