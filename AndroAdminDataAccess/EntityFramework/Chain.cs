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
    
    public partial class Chain
    {
        public Chain()
        {
            this.AMSServerChains = new HashSet<AMSServerChain>();
            this.AndroWebOrderingWebsites = new HashSet<AndroWebOrderingWebsite>();
            this.ChainChains = new HashSet<ChainChain>();
            this.ChainChains1 = new HashSet<ChainChain>();
            this.FTPSiteChains = new HashSet<FTPSiteChain>();
            this.Stores = new HashSet<Store>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Culture { get; set; }
        public Nullable<int> MasterMenuId { get; set; }
    
        public virtual ICollection<AMSServerChain> AMSServerChains { get; set; }
        public virtual ICollection<AndroWebOrderingWebsite> AndroWebOrderingWebsites { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<ChainChain> ChainChains { get; set; }
        public virtual ICollection<ChainChain> ChainChains1 { get; set; }
        public virtual ICollection<FTPSiteChain> FTPSiteChains { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
    }
}
