using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccess.DataAccess
{
    public interface ISiteDetailsDataAccess
    {
        string GetBySiteId(Guid siteId, DataTypeEnum dataType, out SiteDetails siteDetails);
        string GetByExternalSiteIdMyAndromedaUserId(string externalSiteId, string myAndromedaUserId, out SiteDetails siteDetails);
        string Update(string myAndromedaUserId, SiteDetails siteDetails);
    }
}
