//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceEcommerce.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class carrinhoItens
    {
        public int carrinhoItens_id { get; set; }
        public Nullable<int> carrinhoItens_carrinho_id { get; set; }
        public Nullable<int> carrinhoItens_produto_id { get; set; }
        public Nullable<decimal> carrinhoItens_valorUnitario { get; set; }
        public Nullable<decimal> carrinhoItens_valorTotalItem { get; set; }
        public Nullable<int> carrinhoItens_quantidade { get; set; }
        public Nullable<System.DateTime> carrinhoItens_dataCadastro { get; set; }
    }
}
