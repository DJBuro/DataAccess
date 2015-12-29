using AndroCloudDataAccess.Domain;
using System;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IMenuDataAccess
    {
        string Put(Guid siteGuid, string licenseKey, string hardwareKey, string data, int version, DataTypeEnum dataType);
        string Get(Guid securityGuid, Guid siteGuid, DataTypeEnum dataType, out SiteMenu siteMenu);
    }
}
