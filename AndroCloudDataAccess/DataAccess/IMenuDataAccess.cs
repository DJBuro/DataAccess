using AndroCloudDataAccess.Domain;
using System;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IMenuDataAccess
    {
        string Put(Guid securityGuid, int siteID, string data, int version, DataTypeEnum dataType);
        string Get(Guid securityGuid, int siteID, DataTypeEnum dataType, out SiteMenu siteMenu);
    }
}
