﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TeamKaminariAdmin.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class TeamKaminariAdminEntities : DbContext
    {
        public TeamKaminariAdminEntities()
            : base("name=TeamKaminariAdminEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Belt> Belts { get; set; }
        public virtual DbSet<CustomerBelt> CustomerBelts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
    
        public virtual ObjectResult<SelectCustomers_Result> SelectCustomers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SelectCustomers_Result>("SelectCustomers");
        }
    }
}
