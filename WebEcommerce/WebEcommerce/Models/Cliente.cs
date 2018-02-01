using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEcommerce.Models
{
    public class Cliente
    {
        public int cliente_id { get; set; }
        public string cliente_nome { get; set; }
        public string cliente_email { get; set; }
        public DateTime cliente_dataCadastro { get; set; }

        public Cliente(int cliente_id, string cliente_nome, string cliente_email, DateTime cliente_dataCadastro)
        {
            this.cliente_dataCadastro = cliente_dataCadastro;
            this.cliente_email = cliente_email;
            this.cliente_id = cliente_id;
            this.cliente_nome = cliente_nome;
        }
    }
}