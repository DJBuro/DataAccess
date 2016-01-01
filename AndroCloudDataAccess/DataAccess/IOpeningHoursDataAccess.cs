using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IOpeningHoursDataAccess
    {
        string DeleteByIdMyAndromedaUserId(Guid openingHoursId, string myAndromedaUserId);
        string AddByMyAndromedaUserId(TimeSpanBlock timeSpanBlock, string externalSiteId, string myAndromedaUserId);
    }
}
