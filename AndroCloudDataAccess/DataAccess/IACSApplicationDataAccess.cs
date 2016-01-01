using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IACSApplicationDataAccess
    {
        string ConnectionStringOverride { get; set; }
        string Get(string externalApplicationId, out ACSApplication acsApplication);
        string GetById(int id, out ACSApplication acsApplication);
        bool StoreExists(Guid existingSiteId, int acsApplicationId);
    }
}
