using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AndroAdminDataAccess.Domain
{
    public class Partner
    {
        public virtual int Id { get; set; }
        
        [Required]
        [Display(Name="Name")]
        public virtual string Name { get; set; }

        [Required]
        [Display(Name = "Partner id")]
        public virtual string ExternalId { get; set; }

        public virtual int DataVersion { get; set; }
    }
}