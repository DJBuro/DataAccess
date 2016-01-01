﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AndroAdminDataAccess.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AndroAdminEntities : DbContext
    {
        public AndroAdminEntities()
            : base("name=AndroAdminEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<HubAddress> HubAddresses { get; set; }
        public DbSet<ACSApplication> ACSApplications { get; set; }
        public DbSet<ACSApplicationSite> ACSApplicationSites { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AMSServer> AMSServers { get; set; }
        public DbSet<AMSServerChain> AMSServerChains { get; set; }
        public DbSet<Chain> Chains { get; set; }
        public DbSet<ChainChain> ChainChains { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<FTPSite> FTPSites { get; set; }
        public DbSet<FTPSiteChain> FTPSiteChains { get; set; }
        public DbSet<FTPSiteType> FTPSiteTypes { get; set; }
        public DbSet<Host> Hosts { get; set; }
        public DbSet<HostType> HostTypes { get; set; }
        public DbSet<HostV2> HostV2 { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<OpeningHour> OpeningHours { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Site_AMS_upload> Site_AMS_upload { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreAMSServer> StoreAMSServers { get; set; }
        public DbSet<StoreAMSServerFtpSite> StoreAMSServerFtpSites { get; set; }
        public DbSet<StoreHubReset> StoreHubResets { get; set; }
        public DbSet<StoreMenu> StoreMenus { get; set; }
        public DbSet<StoreMenuThumbnail> StoreMenuThumbnails { get; set; }
        public DbSet<StorePaymentProvider> StorePaymentProviders { get; set; }
        public DbSet<StoreStatu> StoreStatus { get; set; }
    }
}
