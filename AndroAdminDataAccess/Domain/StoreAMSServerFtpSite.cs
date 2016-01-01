using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroAdminDataAccess.Domain
{
    public class StoreAMSServerFtpSite
    {
        public virtual int Id { get; set; }
        public virtual StoreAMSServer StoreAMSServer { get; set; }
        public virtual FTPSite FTPSite { get; set; }
    }
}
