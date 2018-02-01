using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEcommerce.Models
{
    public class DetalhesPedido
    {
        public string produto_nome { get; set; }
        public string produto_desc { get; set; }
        public decimal carrinhoItens_valorUnitario { get; set; }
        public int carrinhoItens_quantidade { get; set; }
        public decimal carrinhoItens_valorTotalItem { get; set; }
        public DateTime carrinhoItens_dataCadastro { get; set; }
    }
}