using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace AndroAdminDataAccess.Domain
{
    public class AMSServer
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage="Please enter a name for the AMS server")]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description for the AMS server")]
        public virtual string Description { get; set; }
    }
}
