using AndroCloudDataAccess.Domain;
using System;
using AndroCloudHelper;

namespace AndroCloudDataAccess.DataAccess
{
    public interface ISiteMenuDataAccess
    {
        string ConnectionStringOverride { get; set; }
        string Put(Guid siteId, string licenseKey, string hardwareKey, string data, int version, DataTypeEnum dataType);
        
        string GetBySiteId(Guid siteId, DataTypeEnum dataType, out SiteMenu siteMenu);

        string UpdateThumbnailData(Guid siteId, string data, DataTypeEnum dataType);
    }
}
