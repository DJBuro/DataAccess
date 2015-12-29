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
        public DbSet<MenuItemThumbnail> MenuItemThumbnails { get; set; }
        public DbSet<SiteMenuMediaServerAuthentication> SiteMenuMediaServerAuthentications { get; set; }
        public DbSet<SiteMenuMediaServer> SiteMenuMediaServers { get; set; }
        public DbSet<SiteMenuMediaProfile> SiteMenuMediaProfiles { get; set; }
        public DbSet<SiteMenuMediaProfileSize> SiteMenuMediaProfileSizes { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<SiteMenu> SiteMenus { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserChain> UserChains { get; set; }
        public DbSet<UserRecord> UserRecords { get; set; }
        public DbSet<UserStore> UserStores { get; set; }
        public DbSet<MenuItemThumbnailsLinkTable> MenuItemThumbnailsLinkTable { get; set; }
        public DbSet<EnrolmentLevel> EnrolmentLevels { get; set; }
        public DbSet<MenuItemInGroup> MenuItemInGroups { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<StoreView> StoreViews { get; set; }
        public DbSet<StoreEnrolment> StoreEnrolments { get; set; }
        public DbSet<RolePermissions> RolePermissions { get; set; }
        public DbSet<CloudSynchronizationTask> CloudSynchronizationTasks { get; set; }
    }
}
