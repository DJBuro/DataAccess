using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AndroAdminDataAccess.Domain
{
    public class Host
    {
        public virtual int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public virtual string Name { get; set; }

        [Required]
        [Display(Name="Host Name")]
        public virtual string HostName { get; set; }

        [Required]
        [Display(Name="Priority")]
        public virtual int Order { get; set; }

        [Required]
        [Display(Name="Private host name")]
        public virtual string PrivateHostName { get; set; }

   }
}