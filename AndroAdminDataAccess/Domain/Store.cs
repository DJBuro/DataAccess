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
        [Display(Name="Name")]
        public virtual string Name { get; set; }

        [Display(Name = "Andromeda store id")]
        public virtual int AndromedaSiteId { get; set; }

        [Display(Name = "Customer store id")]
        public virtual string CustomerSiteId { get; set; }

        [Display(Name = "Last uploaded date/time")]
        public virtual DateTime? LastFTPUploadDateTime { get; set; }

        [Display(Name = "Store status")]
        public virtual StoreStatus StoreStatus { get; set; }

        [Display(Name = "External site id")]
        public virtual string ExternalSiteId { get; set; }

        [Display(Name = "External site name")]
        public virtual string ExternalSiteName { get; set; }

        [Display(Name = "Country")]
        public Country Country { get; set; }

        public Store()
        {
            this.Name = "";
            this.AndromedaSiteId = 0;
            this.CustomerSiteId = "";
            this.LastFTPUploadDateTime = null;
            this.StoreStatus = null;
            this.ExternalSiteId = Guid.NewGuid().ToString().Replace("{", "").Replace("}", "").ToUpper();
            this.ExternalSiteName = "";
            this.Country = null;
        }
   }
}