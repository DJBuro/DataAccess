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
        public virtual string Name { get; set; }
        public virtual int AndromedaSiteId { get; set; }
        public virtual string CustomerSiteId { get; set; }
        public virtual DateTime? LastFTPUploadDateTime { get; set; }
    }
}