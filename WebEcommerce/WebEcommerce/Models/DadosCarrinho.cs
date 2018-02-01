using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEcommerce.Models
{
    public class DadosCarrinho
    {
        public int carrinho_id { get; set; }
        public string produto_nome { get; set; }
        public string produto_desc { get; set; }
        public Nullable<decimal> carrinhoItens_valorUnitario { get; set; }
        public Nullable<decimal> carrinhoItens_valorTotalItem { get; set; }
    }
}