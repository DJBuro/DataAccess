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
    
    public partial class Store
    {
        public Store()
        {
            this.ACSApplicationSites = new HashSet<ACSApplicationSite>();
            this.OpeningHours = new HashSet<OpeningHour>();
            this.StoreAMSServers = new HashSet<StoreAMSServer>();
            this.StoreHubResets = new HashSet<StoreHubReset>();
            this.StoreMenus = new HashSet<StoreMenu>();
            this.HostV2 = new HashSet<HostV2>();
            this.HubAddresses = new HashSet<HubAddress>();
            this.StoreMenuThumbnails = new HashSet<StoreMenuThumbnail>();
            this.StoreDevices = new HashSet<StoreDevice>();
            this.DeliveryAreas = new HashSet<DeliveryArea>();
            this.StoreHostV2ApiCredentials = new HashSet<StoreHostV2ApiCredentials>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int AndromedaSiteId { get; set; }
        public string CustomerSiteId { get; set; }
        public Nullable<System.DateTime> LastFTPUploadDateTime { get; set; }
        public int StoreStatusId { get; set; }
        public string ClientSiteName { get; set; }
        public string ExternalSiteName { get; set; }
        public string ExternalId { get; set; }
        public Nullable<int> EstimatedDeliveryTime { get; set; }
        public string TimeZone { get; set; }
        public string Telephone { get; set; }
        public string LicenseKey { get; set; }
        public Nullable<int> AddressId { get; set; }
        public Nullable<int> StorePaymentProviderID { get; set; }
        public int DataVersion { get; set; }
        public int ChainId { get; set; }
    
        public virtual ICollection<ACSApplicationSite> ACSApplicationSites { get; set; }
        public virtual Address Address { get; set; }
        public virtual Chain Chain { get; set; }
        public virtual ICollection<OpeningHour> OpeningHours { get; set; }
        public virtual StorePaymentProvider StorePaymentProvider { get; set; }
        public virtual StoreStatu StoreStatu { get; set; }
        public virtual ICollection<StoreAMSServer> StoreAMSServers { get; set; }
        public virtual ICollection<StoreHubReset> StoreHubResets { get; set; }
        public virtual ICollection<StoreMenu> StoreMenus { get; set; }
        public virtual ICollection<HostV2> HostV2 { get; set; }
        public virtual ICollection<HubAddress> HubAddresses { get; set; }
        public virtual ICollection<StoreMenuThumbnail> StoreMenuThumbnails { get; set; }
        public virtual ICollection<StoreDevice> StoreDevices { get; set; }
        public virtual ICollection<DeliveryArea> DeliveryAreas { get; set; }
        public virtual ICollection<StoreHostV2ApiCredentials> StoreHostV2ApiCredentials { get; set; }
    }
}
