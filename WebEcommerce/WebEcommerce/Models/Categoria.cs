using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEcommerce.Models
{
    public class Categoria
    {
        public int categoria_id { get; set; }
        public string categoria_nome { get; set; }
        public bool categoria_ativo { get; set; }
        public DateTime categoria_dataCadastro { get; set; }
    }
}