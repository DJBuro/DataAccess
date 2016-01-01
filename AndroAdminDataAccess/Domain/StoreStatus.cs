using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AndroAdminDataAccess.Domain
{
    public class StoreStatus
    {
        public virtual int Id { get; set; }
        public virtual string Status { get; set; }
        public virtual string Description { get; set; }
    }
}