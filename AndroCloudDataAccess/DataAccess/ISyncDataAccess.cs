using System;
using System.Linq;
using CloudSyncModel;

namespace AndroCloudDataAccess.DataAccess
{
    public interface ISyncDataAccess
    {
        string ConnectionStringOverride { get; set; }
        string Sync(SyncModel syncModel, Action<string> successAction, Action<string> failureAction);
    }
}
