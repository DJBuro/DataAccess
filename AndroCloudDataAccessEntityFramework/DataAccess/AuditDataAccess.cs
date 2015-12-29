using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using AndroCloudDataAccessEntityFramework.Model;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class AuditDataAccess : IAuditDataAccess
    {
        public string ConnectionStringOverride { get; set; }

        public string Add(string sourceId, string hardwareId, string ipPort, string action, int responseTime, int? errorCode)
        {
            //using (ACSEntities acsEntities = ConnectionStringOverride == null ? new ACSEntities() : new ACSEntities(this.ConnectionStringOverride))
            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                Audit audit = new Audit
                {
                    SrcID = sourceId,
                    HardwareID = hardwareId,
                    IP_Port = ipPort,
                    Action = action,
                    ResponseTime = responseTime,
                    ID = Guid.NewGuid(),
                    DateCreated = DateTime.UtcNow,
                    ErrorCode = errorCode
                };

                acsEntities.Audits.Add(audit);

                acsEntities.SaveChanges();
            }

            return "";
        }
    }
}
