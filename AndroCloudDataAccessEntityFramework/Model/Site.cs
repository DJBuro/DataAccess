//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AndroCloudDataAccessEntityFramework.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Site
    {
        public Site()
        {
            this.ACSApplicationSites = new HashSet<ACSApplicationSite>();
            this.OpeningHours = new HashSet<OpeningHour>();
            this.Orders = new HashSet<Order>();
            this.SiteMenus = new HashSet<SiteMenu>();
        }
    
        public System.Guid ID { get; set; }
        public Nullable<System.Guid> SignalRConnectionID { get; set; }
        public Nullable<System.Guid> SessionID { get; set; }
        public string ExternalSiteName { get; set; }
        public int AndroID { get; set; }
        public Nullable<System.Guid> AddressID { get; set; }
        public Nullable<bool> StoreConnected { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
        public string ExternalId { get; set; }
        public Nullable<int> EstimatedDeliveryTime { get; set; }
        public string TimeZone { get; set; }
        public string Telephone { get; set; }
        public string LicenceKey { get; set; }
        public System.Guid SiteStatusID { get; set; }
        public Nullable<int> StorePaymentProviderId { get; set; }
    
        public virtual ICollection<ACSApplicationSite> ACSApplicationSites { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<OpeningHour> OpeningHours { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<SiteMenu> SiteMenus { get; set; }
        public virtual SiteStatus SiteStatus { get; set; }
    }
}
