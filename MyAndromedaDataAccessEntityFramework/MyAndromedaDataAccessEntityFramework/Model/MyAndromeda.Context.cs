﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyAndromedaDataAccessEntityFramework.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MyAndromedaEntities : DbContext
    {
        public MyAndromedaEntities()
            : base("name=MyAndromedaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<CustomerRecord> CustomerRecords { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<EmailCampaignAudit> EmailCampaignAudits { get; set; }
        public DbSet<EmailCampaignSite> EmailCampaignSites { get; set; }
        public DbSet<EmailCampaign> EmailCampaigns { get; set; }
        public DbSet<EmailCampaignSentEmail> EmailCampaignSentEmails { get; set; }
        public DbSet<EmailCampaignSetting> EmailCampaignSettings { get; set; }
    }
}
