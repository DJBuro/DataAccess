﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AndroCloudDataAccessEntityFramework.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ACSEntities : DbContext
    {
        public ACSEntities()
            : base("name=ACSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ACSApplication> ACSApplications { get; set; }
        public DbSet<ACSApplicationSite> ACSApplicationSites { get; set; }
        public DbSet<ACSLog> ACSLogs { get; set; }
        public DbSet<ACSQueue> ACSQueues { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Host> Hosts { get; set; }
        public DbSet<OpeningHour> OpeningHours { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatu> OrderStatus { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<SiteMenu> SiteMenus { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<SiteStatus> SiteStatuses { get; set; }
        public DbSet<StorePaymentProvider> StorePaymentProviders { get; set; }
    }
}
