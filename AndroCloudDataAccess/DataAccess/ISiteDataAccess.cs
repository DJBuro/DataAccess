﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccess.DataAccess
{
    public interface ISiteDataAccess
    {
        string Get(Guid securityGuid, Guid? chainGuid, float? maxDistance, float? longitude, float? latitude, out List<Site> sites);
    }
}
