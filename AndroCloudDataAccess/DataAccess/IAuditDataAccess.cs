using AndroCloudDataAccess.Domain;
using System;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IAuditDataAccess
    {
        string ConnectionStringOverride { get; set; }
        string Add(string sourceId, string hardwareId, string ipPort, string action, int responseTime);
    }
}
