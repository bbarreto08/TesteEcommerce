using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEcommerce.Models
{
    public class DadosInserirCarrinho
    {
        public Carrinho carrinho { get; set; }
        public CarrinhoItens carrinhoItens { get; set; }
        
        public DadosInserirCarrinho(Carrinho carrinho, CarrinhoItens carrinhoItens)
        {
            this.carrinho = carrinho;
            this.carrinhoItens = carrinhoItens;
        }
    }
}