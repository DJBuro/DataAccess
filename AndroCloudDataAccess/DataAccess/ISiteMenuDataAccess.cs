﻿using AndroCloudDataAccess.Domain;
using System;

namespace AndroCloudDataAccess.DataAccess
{
    public interface ISiteMenuDataAccess
    {
        string Put(Guid siteId, string licenseKey, string hardwareKey, string data, int version, DataTypeEnum dataType);
        string GetBySiteId(Guid siteId, DataTypeEnum dataType, out SiteMenu siteMenu);
    }
}
