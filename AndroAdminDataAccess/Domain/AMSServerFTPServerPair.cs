using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroAdminDataAccess.Domain
{
    public class AMSServerFTPServerPair
    {
        public virtual int Id { get; set; }
        public virtual int Priority { get; set; }
        public virtual Store Store { get; set; }
        public virtual AMSServer AMSServer { get; set; }
        public virtual FTPSite PrimaryFTPSite { get; set; }
        public virtual FTPSite SecondaryFTPSite{ get; set; }
    }
}
