﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AndroUsersDataAccess.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AndroUsersEntities : DbContext
    {
        public AndroUsersEntities()
            : base("name=AndroUsersEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<tbl_AndroUser> tbl_AndroUser { get; set; }
        public DbSet<tbl_AndroUserPermission> tbl_AndroUserPermission { get; set; }
        public DbSet<tbl_Project> tbl_Project { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<SecurityGroupUser> SecurityGroupUsers { get; set; }
        public DbSet<SecurityGroupPermission> SecurityGroupPermissions { get; set; }
        public DbSet<SecurityGroup> SecurityGroups { get; set; }
    }
}
