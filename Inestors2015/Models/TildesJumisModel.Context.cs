﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Investors2015.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TildesJumisFinancialDBContext : DbContext
    {
        public TildesJumisFinancialDBContext()
            : base("name=TildesJumisFinancialDBContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<DocumentGroup> DocumentGroups { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<FinancialDoc> FinancialDocs { get; set; }
        public virtual DbSet<FinancialDocLine> FinancialDocLines { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }
    }
}
