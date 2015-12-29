using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DashboardDataAccess.Domain
{
    public class AMSServerFTPServerPair
    {
        public virtual int Id { get; set; }
        public virtual int Priority { get; set; }
        public virtual string AMSServer { get; set; }
        public virtual string PrimaryAMSServer { get; set; }
        public virtual string SecondaryAMSServer { get; set; }
    }
}
