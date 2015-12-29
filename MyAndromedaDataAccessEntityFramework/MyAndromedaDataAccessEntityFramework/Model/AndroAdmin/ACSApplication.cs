//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyAndromedaDataAccessEntityFramework.Model.AndroAdmin
{
    using System;
    using System.Collections.Generic;
    
    public partial class ACSApplication
    {
        public ACSApplication()
        {
            this.ACSApplicationSites = new HashSet<ACSApplicationSite>();
            this.HostV2 = new HashSet<HostV2>();
            this.Hosts = new HashSet<Host>();
        }
    
        public int Id { get; set; }
        public string ExternalApplicationId { get; set; }
        public string Name { get; set; }
        public string ExternalDisplayName { get; set; }
        public int PartnerId { get; set; }
        public int DataVersion { get; set; }
    
        public virtual Partner Partner { get; set; }
        public virtual ICollection<ACSApplicationSite> ACSApplicationSites { get; set; }
        public virtual ICollection<HostV2> HostV2 { get; set; }
        public virtual ICollection<Host> Hosts { get; set; }
    }
}
