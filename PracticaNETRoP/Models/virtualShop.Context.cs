﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PracticaNETRoP.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VirtualShopEntities : DbContext
    {
        public VirtualShopEntities()
            : base("name=VirtualShopEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<Products> Products { get; set; }
    }
}
