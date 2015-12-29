using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AndroAdminDataAccess.Domain
{
    public class StoreListItem
    {
        public virtual int Id { get; set; }
        
        [Required]
        [Display(Name="Name")]
        public virtual string Name { get; set; }

        [Display(Name = "Andromeda store id")]
        public virtual int AndromedaSiteId { get; set; }

        [Display(Name = "Customer store id")]
        public virtual string CustomerSiteId { get; set; }

        [Display(Name = "Store status")]
        public virtual string StoreStatus { get; set; }

        public StoreListItem()
        {
            this.Name = "";
            this.AndromedaSiteId = 0;
            this.CustomerSiteId = "";
            this.StoreStatus = "";
        }
   }
}