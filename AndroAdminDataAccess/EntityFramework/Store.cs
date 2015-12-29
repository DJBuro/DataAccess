//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AndroAdminDataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Store
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Store()
        {
            this.ACSApplicationSites = new HashSet<ACSApplicationSite>();
            this.Chains = new HashSet<Chain>();
            this.DeliveryAreas = new HashSet<DeliveryArea>();
            this.DeliveryZoneNames = new HashSet<DeliveryZoneName>();
            this.OpeningHours = new HashSet<OpeningHour>();
            this.StoreAMSServers = new HashSet<StoreAMSServer>();
            this.StoreHostV2ApiCredentials = new HashSet<StoreHostV2ApiCredentials>();
            this.StoreHubResets = new HashSet<StoreHubReset>();
            this.StoreMenus = new HashSet<StoreMenu>();
            this.StoreMenuThumbnails = new HashSet<StoreMenuThumbnail>();
            this.StoreDevices = new HashSet<StoreDevice>();
            this.HostV2 = new HashSet<HostV2>();
            this.HubAddresses = new HashSet<HubAddress>();
            this.StoreLoyalties = new HashSet<StoreLoyalty>();
            this.StoreDrivers = new HashSet<StoreDriver>();
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
        public string TimeZoneInfoId { get; set; }
        public string UiCulture { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACSApplicationSite> ACSApplicationSites { get; set; }
        public virtual Address Address { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chain> Chains { get; set; }
        public virtual Chain Chain { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryArea> DeliveryAreas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryZoneName> DeliveryZoneNames { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OpeningHour> OpeningHours { get; set; }
        public virtual StorePaymentProvider StorePaymentProvider { get; set; }
        public virtual StoreStatu StoreStatu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreAMSServer> StoreAMSServers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreHostV2ApiCredentials> StoreHostV2ApiCredentials { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreHubReset> StoreHubResets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreMenu> StoreMenus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreMenuThumbnail> StoreMenuThumbnails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreDevice> StoreDevices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HostV2> HostV2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HubAddress> HubAddresses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreLoyalty> StoreLoyalties { get; set; }
        public virtual StoreGPSSetting StoreGPSSetting { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreDriver> StoreDrivers { get; set; }
    }
}
