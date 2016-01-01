﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;
using AndroCloudHelper;
using CloudSyncModel;

namespace AndroCloudDataAccess.DataAccess
{
    public interface ISyncDataAccess
    {
        string Sync(SyncModel syncModel);
    }
}
