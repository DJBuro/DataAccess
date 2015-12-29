﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
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
    
        public virtual DbSet<HubAddress> HubAddresses { get; set; }
        public virtual DbSet<ACSApplication> ACSApplications { get; set; }
        public virtual DbSet<ACSApplicationSite> ACSApplicationSites { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AMSServer> AMSServers { get; set; }
        public virtual DbSet<AMSServerChain> AMSServerChains { get; set; }
        public virtual DbSet<AndroWebOrderingSubscriptionType> AndroWebOrderingSubscriptionTypes { get; set; }
        public virtual DbSet<AndroWebOrderingTheme> AndroWebOrderingThemes { get; set; }
        public virtual DbSet<AndroWebOrderingWebsite> AndroWebOrderingWebsites { get; set; }
        public virtual DbSet<Chain> Chains { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Day> Days { get; set; }
        public virtual DbSet<DeliveryArea> DeliveryAreas { get; set; }
        public virtual DbSet<DeliveryZoneName> DeliveryZoneNames { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<ExternalApi> ExternalApis { get; set; }
        public virtual DbSet<FTPSite> FTPSites { get; set; }
        public virtual DbSet<FTPSiteChain> FTPSiteChains { get; set; }
        public virtual DbSet<FTPSiteType> FTPSiteTypes { get; set; }
        public virtual DbSet<Host> Hosts { get; set; }
        public virtual DbSet<HostType> HostTypes { get; set; }
        public virtual DbSet<HostV2> HostV2 { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<OpeningHour> OpeningHours { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }
        public virtual DbSet<PostCodeSector> PostCodeSectors { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Site_AMS_upload> Site_AMS_upload { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<StoreAMSServer> StoreAMSServers { get; set; }
        public virtual DbSet<StoreAMSServerFtpSite> StoreAMSServerFtpSites { get; set; }
        public virtual DbSet<StoreDevice> StoreDevices { get; set; }
        public virtual DbSet<StoreHostV2ApiCredentials> StoreHostV2ApiCredentials { get; set; }
        public virtual DbSet<StoreHubReset> StoreHubResets { get; set; }
        public virtual DbSet<StoreMenu> StoreMenus { get; set; }
        public virtual DbSet<StoreMenuThumbnail> StoreMenuThumbnails { get; set; }
        public virtual DbSet<StorePaymentProvider> StorePaymentProviders { get; set; }
        public virtual DbSet<StoreStatu> StoreStatus { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<StoreLoyalty> StoreLoyalties { get; set; }
        public virtual DbSet<ChainChain> ChainChains { get; set; }
        public virtual DbSet<StoreDriver> StoreDrivers { get; set; }
        public virtual DbSet<StoreGPSSetting> StoreGPSSettings { get; set; }
    }
}
