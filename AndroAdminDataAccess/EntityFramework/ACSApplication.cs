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
    
    public partial class ACSApplication
    {
        public ACSApplication()
        {
            this.ACSApplicationSites = new HashSet<ACSApplicationSite>();
        }
    
        public int Id { get; set; }
        public string ExternalApplicationId { get; set; }
        public string Name { get; set; }
        public int PartnerId { get; set; }
        public int DataVersion { get; set; }
    
        public virtual Partner Partner { get; set; }
        public virtual ICollection<ACSApplicationSite> ACSApplicationSites { get; set; }
    }
}
