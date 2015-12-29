using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;
using AndroCloudHelper;

namespace AndroCloudDataAccess.DataAccess
{
    public interface ISiteDataAccess
    {
        string GetByFilter(
            Guid partnerId,
            Guid? chainId,
            float? maxDistance,
            float? longitude,
            float? latitude,
            DataTypeEnum dataType,
            out List<Site> sites);

        string GetByExternalSiteId(string externalSiteId, out Site site);

        string GetByAndromedaSiteId(int andromedaSiteId, out AndroCloudDataAccess.Domain.Site site);

        string GetByIdAndPartner(Guid partnerId, Guid siteId, out Site site);
    }
}
