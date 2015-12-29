using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace AndroAdminDataAccess.Domain
{
    public class ACSApplication
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Please enter an external Application id")]
        public virtual string ExternalApplicationId { get; set; }

        [Required(ErrorMessage = "Please enter a name for this application")]
        public virtual string Name { get; set; }

        public virtual int PartnerId { get; set; }

        public virtual int DataVersion { get; set; }
    }
}
