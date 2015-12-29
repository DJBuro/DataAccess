using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IStoreAMSServerFTPSiteDAO
    {
        IEnumerable<StoreAMSServerFtpSite> GetAll();
        IEnumerable<StoreAMSServerFtpSite> GetBySiteId(int siteId);
        void Add(StoreAMSServerFtpSite storeAMSServerFtpSite);
        StoreAMSServerFtpSite GetBySiteIdAMSServerIdFTPSiteId(int storeAMSServerId, int ftpSiteId);
    }
}