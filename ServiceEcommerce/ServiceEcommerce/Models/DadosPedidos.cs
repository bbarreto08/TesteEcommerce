using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceEcommerce.Models
{
    public class DadosPedidos
    {
        public Nullable<int> cliente_id { get; set; }
        public int carrinho_id { get; set; }
        public string cliente_nome { get; set; }
        public Nullable<decimal> pedido_valor { get; set; }
        public Nullable<DateTime> pedido_dataCadastro { get; set; }
    }
}