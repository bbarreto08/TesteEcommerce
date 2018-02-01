using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEcommerce.Models
{
    public class Carrinho
    {
        public int carrinho_id { get; set; }
        public DateTime carrinho_dataCadastro { get; set; }
        public int cliente_id { get; set; }
        public Nullable<decimal> carrinho_total { get; set; }
    }
}