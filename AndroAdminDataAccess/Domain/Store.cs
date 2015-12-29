using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AndroAdminDataAccess.Domain
{
    public class Store
    {
        public virtual int Id { get; set; }
        
        [Required]
        [Display(Name="Andromeda store Name")]
        public virtual string Name { get; set; }

        [Required]
        [Display(Name = "Andromeda store ID")]
        public virtual int AndromedaSiteId { get; set; }

        [Required]
        [Display(Name = "Customer store ID")]
        public virtual string CustomerSiteId { get; set; }

        [Display(Name = "Customer store name")]
        public virtual string CustomerSiteName { get; set; }

        [Display(Name = "External site ID")]
        public virtual string ExternalSiteId { get; set; }

        [Display(Name = "External site name")]
        public virtual string ExternalSiteName { get; set; }

        [Display(Name = "Last uploaded date/time")]
        public virtual DateTime? LastFTPUploadDateTime { get; set; }

        [Display(Name = "Store status")]
        public virtual StoreStatus StoreStatus { get; set; }

        [Display(Name = "Address")]
        public Address Address { get; set; }

        [Display(Name = "Telephone")]
        public string Telephone { get; set; }

        [Display(Name = "TimeZone")]
        public string TimeZone { get; set; }

        [Display(Name = "Payment provider")]
        public StorePaymentProvider PaymentProvider { get; set; }

        public List<TimeSpanBlock> OpeningHours { get; set; }

        public Chain Chain { get; set; }

        public Store()
        {
            this.Name = "";
            this.AndromedaSiteId = 0;
            
            this.CustomerSiteId = "";
            this.CustomerSiteName = "";

            this.ExternalSiteId = Guid.NewGuid().ToString().Replace("{", "").Replace("}", "").ToUpper();
            this.ExternalSiteName = "";

            this.LastFTPUploadDateTime = null;
            this.StoreStatus = null;
            this.Address = null;
            this.Telephone = "";
            this.TimeZone = "";
            this.Chain = null;
        }

        public string LicenceKey { get; set; }
    }
}