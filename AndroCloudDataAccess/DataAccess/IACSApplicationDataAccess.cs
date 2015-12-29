using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IACSApplicationDataAccess
    {
        string Get(string externalApplicationId, out ACSApplication acsApplication);
        bool StoreExists(Guid siteId, Guid applicationId);
    }
}
