using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IStoreAMSServerFTPSiteDAO
    {
        string ConnectionStringOverride { get; set; }
        IList<StoreAMSServerFtpSiteListItem> GetAllListItems();
        IList<StoreAMSServerFtpSite> GetAll();
        StoreAMSServerFtpSite GetById(int id);
        IList<StoreAMSServerFtpSite> GetByStoreAMSServerId(int id);
        IList<StoreAMSServerFtpSite> GetBySiteId(int siteId);
        void DeleteByFTPSiteId(int ftpSiteId);
        void DeleteByAMSServerId(int amsServerId);
        void DeleteById(int id);
        void Add(StoreAMSServerFtpSite storeAMSServerFtpSite);
        StoreAMSServerFtpSite GetBySiteIdAMSServerIdFTPSiteId(int storeAMSServerId, int ftpSiteId);
    }
}