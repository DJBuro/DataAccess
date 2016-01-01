using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;
using AndroCloudHelper;
using CloudSyncModel;

namespace AndroCloudDataAccess.DataAccess
{
    public interface ISettingsDataAccess
    {
        string ConnectionStringOverride { get; set; }
        string GetByName(string name, out string value);
    }
}
