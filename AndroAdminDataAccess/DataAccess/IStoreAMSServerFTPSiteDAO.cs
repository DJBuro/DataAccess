using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IStoreAMSServerFTPSiteDAO
    {
        IList<StoreAMSServerFtpSite> GetAll();
        IList<StoreAMSServerFtpSite> GetBySiteId(int siteId);
        void DeleteByFTPSiteId(int ftpSiteId);
        void DeleteByAMSServerId(int amsServerId);
        void Add(StoreAMSServerFtpSite storeAMSServerFtpSite);
        StoreAMSServerFtpSite GetBySiteIdAMSServerIdFTPSiteId(int storeAMSServerId, int ftpSiteId);
    }
}