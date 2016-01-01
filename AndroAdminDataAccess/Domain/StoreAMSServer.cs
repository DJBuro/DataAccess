using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AndroAdminDataAccess.Domain
{
    public class StoreAMSServer
    {
        public virtual int Id { get; set; }
        public virtual Store Store { get; set; }
        public virtual AMSServer AMSServer { get; set; }
        public virtual int Priority { get; set; }
    }
}