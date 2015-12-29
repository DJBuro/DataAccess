﻿using System;
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
        public string Add(string sourceId, string hardwareId, string ipPort, string action, int responseTime)
        {
            ACSEntities acsEntities = new ACSEntities();

            Audit audit = new Audit
            {
                SrcID = sourceId,
                HardwareID = hardwareId,
                IP_Port = ipPort,
                Action = action,
                ResponseTime = responseTime,
                ID = Guid.NewGuid(),
                DateCreated = DateTime.UtcNow
            };

            acsEntities.AddToAudits(audit);

            acsEntities.SaveChanges();

            return "";
        }
    }
}
