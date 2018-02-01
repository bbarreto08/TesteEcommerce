using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEcommerce.Models
{
    public class Produto
    {
        public int produto_id { get; set; }
        public string produto_nome { get; set; }
        public string produto_desc { get; set; }
        public bool produto_ativo { get; set; }
        public decimal produto_preco { get; set; }
        public decimal produto_precoPromo { get; set; }
    }
}