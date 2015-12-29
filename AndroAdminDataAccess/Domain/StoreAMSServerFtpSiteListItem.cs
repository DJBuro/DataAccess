using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroAdminDataAccess.Domain
{
    public class StoreAMSServerFtpSiteListItem
    {
        public virtual int StoreId { get; set; }
        public virtual string StoreName { get; set; }
        public virtual string AMSServerName { get; set; }
        public virtual string FTPSite { get; set; }
        public virtual DateTime? LastUploaded { get; set; }
    }
}
