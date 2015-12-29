using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using DataWarehouseDataAccess.DataAccess;
using DataWarehouseDataAccessEntityFramework.Model;
using DataWarehouseDataAccess.Domain;
using System.Collections.Generic;
using System.Transactions;

namespace DataWarehouseDataAccessEntityFramework.DataAccess
{
    public class TelemetryDataAccess : ITelemetryDataAccess
    {
        public string ConnectionStringOverride { get; set; }

        public string AddTelemetrySession
        (
            int applicationId,
            DataWarehouseDataAccess.Domain.TelemetrySession telemetrySession,
            string externalSiteId
        )
        {
            using (System.Transactions.TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Suppress))
            {
                using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
                {
                    DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);

                    // Create a customer feedback entity
                    Model.AndroWebTelemetrySession androWebTelemetrySessionEntity = new Model.AndroWebTelemetrySession()
                    {
                        Id = telemetrySession.Id,
                        BrowserDetails = telemetrySession.BrowserDetails,
                        CreatedDateTime = (telemetrySession.CreatedDateTime != null ? DateTime.Parse(telemetrySession.CreatedDateTime) : DateTime.UtcNow),
                        CustomerId = null, //telemetrySession.CustomerId,
                        Referrer = telemetrySession.Referrer,
                        ExternalSiteId = externalSiteId
                    };

                    // Add the telemetry session to the database
                    dataWarehouseEntities.AndroWebTelemetrySessions.Add(androWebTelemetrySessionEntity);

                    // Commit the telemetry session
                    dataWarehouseEntities.SaveChanges();
                }
            }

            return "";
        }

        public string AddTelemetry
        (
            int applicationId,
            DataWarehouseDataAccess.Domain.Telemetry telemetry
        )
        {
            using (System.Transactions.TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Suppress))
            {
                using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
                {
                    DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);

                    // Create a customer feedback entity
                    Model.AndroWebTelemetry androWebTelemetryEntity = new Model.AndroWebTelemetry()
                    {
                        Id = Guid.NewGuid(),
                        Action = telemetry.Action,
                        AndroWebSessionID = Guid.Parse(telemetry.SessionId),
                        LoggedDateTime = DateTime.Parse(telemetry.DateTime)
                    };

                    // Add the telemetry to the database
                    dataWarehouseEntities.AndroWebTelemetries.Add(androWebTelemetryEntity);

                    // Commit the telemetry
                    dataWarehouseEntities.SaveChanges();
                }
            }

            return "";
        }
    }
}
