using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceEcommerce.Models
{
    public class DetalhesPedido
    {
        public string produto_nome { get; set; }
        public string produto_desc { get; set; }
        public Nullable<decimal> carrinhoItens_valorUnitario { get; set; }
        public Nullable<int> carrinhoItens_quantidade { get; set; }
        public Nullable<decimal> carrinhoItens_valorTotalItem { get; set; }
        public Nullable<DateTime> carrinhoItens_dataCadastro { get; set; }
    }
}
