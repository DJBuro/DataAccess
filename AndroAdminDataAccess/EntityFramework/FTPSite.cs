//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class FTPSite
    {
        public FTPSite()
        {
            this.StoreAMSServerFtpSites = new HashSet<StoreAMSServerFtpSite>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Port { get; set; }
        public string ServerType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsPrimary { get; set; }
        public int FTPSiteType_Id { get; set; }
    
        public virtual FTPSiteType FTPSiteType { get; set; }
        public virtual ICollection<StoreAMSServerFtpSite> StoreAMSServerFtpSites { get; set; }
    }
}
