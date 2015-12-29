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
        string ConnectionStringOverride { get; set; }

        string GetByFilter(
            int applicationId,
            float? maxDistance,
            double? longitude,
            double? latitude,
            DataTypeEnum dataType,
            out List<Site> sites);

        string GetByExternalSiteId(string externalSiteId, out Site site);

        string GetByAndromedaSiteIdAndLive(int andromedaSiteId, out AndroCloudDataAccess.Domain.Site site);

        string GetByAndromedaSiteId(int andromedaSiteId, out AndroCloudDataAccess.Domain.Site site);

        string GetByIdAndApplication(int applicationId, Guid siteId, out Site site);

        string Update(int andromedaSiteId, int etd);
    }
}
