﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataWarehouseDataAccessEntityFramework.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DataWarehouseEntities : DbContext
    {
        public DataWarehouseEntities()
            : base("name=DataWarehouseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<MarketingLevel> MarketingLevels { get; set; }
        public DbSet<modifier> modifiers { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<OrderStatu> OrderStatus { get; set; }
        public DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }
        public DbSet<PasswordResetRequest> PasswordResetRequests { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<SiteVoucher> SiteVouchers { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<UsedVoucher> UsedVouchers { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Applications_vw> Applications_vw { get; set; }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<ACSErrorCode> ACSErrorCodes { get; set; }
    }
}
