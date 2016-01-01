using System;
using System.Linq;
using CloudSyncModel;
using System.Collections.Generic;

namespace AndroCloudDataAccess.DataAccess
{
    public interface ISyncDataAccess
    {
        string ConnectionStringOverride { get; set; }
        string Sync(SyncModel syncModel, Action<string> successAction, Action<string> failureAction);
    }
}
