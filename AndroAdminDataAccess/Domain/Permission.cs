using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AndroAdminDataAccess.Domain
{
    public class Permission
    {
        public virtual int Id { get; set; }
        
        [Required]
        [Display(Name="Permission Name")]
        public virtual string Name { get; set; }

        public Permission()
        {
            this.Name = "";
        }
   }
}