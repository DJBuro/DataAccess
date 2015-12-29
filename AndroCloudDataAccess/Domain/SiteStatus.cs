using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroCloudDataAccess.Domain
{
    public class SiteStatus
    {
        public virtual int Id { get; set; }
        public virtual string Status { get; set; }
        public virtual string Description { get; set; }
    }
}
