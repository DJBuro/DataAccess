using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccess.DataAccess
{
    public interface ISiteDataAccess
    {
        List<Site> Get(Guid securityGuidstring, Guid? chainGuidText, float? maxDistance, float? longitude, float? latitude);
    }
}
