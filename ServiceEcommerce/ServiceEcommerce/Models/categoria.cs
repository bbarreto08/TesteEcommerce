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
    
    public partial class categoria
    {
        public int categoria_id { get; set; }
        public string categoria_nome { get; set; }
        public Nullable<bool> categoria_ativo { get; set; }
        public Nullable<System.DateTime> categoria_dataCadastro { get; set; }
    }
}