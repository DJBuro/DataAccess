﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyAndromedaDataAccessEntityFramework.Model.MyAndromeda
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MyAndromedaDbContext : DbContext
    {
        public MyAndromedaDbContext()
            : base("name=MyAndromedaDbContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<EmailCampaign> EmailCampaigns { get; set; }
        public DbSet<EmailCampaignAudit> EmailCampaignAudits { get; set; }
        public DbSet<EmailCampaignSentEmail> EmailCampaignSentEmails { get; set; }
        public DbSet<EmailCampaignSetting> EmailCampaignSettings { get; set; }
        public DbSet<EmailCampaignSite> EmailCampaignSites { get; set; }
        public DbSet<EmailCampaignTask> EmailCampaignTasks { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuItemGroup> MenuItemGroups { get; set; }
        public DbSet<MenuItemThumbnail> MenuItemThumbnails { get; set; }
        public DbSet<SiteMenu> SiteMenus { get; set; }
        public DbSet<SiteMenuMediaServer> SiteMenuMediaServers { get; set; }
        public DbSet<SiteMenuMediaServerAuthentication> SiteMenuMediaServerAuthentications { get; set; }
    }
}
