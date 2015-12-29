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

                    // Create a telemetry entity
                    Model.AndroWebTelemetry androWebTelemetryEntity = new Model.AndroWebTelemetry()
                    {
                        Id = Guid.NewGuid(),
                        Action = telemetry.Action,
                        AndroWebSessionID = Guid.Parse(telemetry.SessionId),
                        LoggedDateTime = DateTime.Parse(telemetry.DateTime),
                        ExtraInfo = telemetry.ExtraInfo
                    };

                    // Add the telemetry to the database
                    dataWarehouseEntities.AndroWebTelemetries.Add(androWebTelemetryEntity);

                    // Commit the telemetry
                    dataWarehouseEntities.SaveChanges();
                }
            }

            return "";
        }

        public string UpdateTelemetrySession
        (
            int applicationId,
            string customerAccountId,
            string sessionId
        )
        {
            using (System.Transactions.TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Suppress))
            {
                using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
                {
                    DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);

                    // Lookup the customer account using the username in extrainfo
                    var customerQuery =
                    from c in dataWarehouseEntities.Customers
                    join ca in dataWarehouseEntities.CustomerAccounts
                        on c.CustomerAccountId equals ca.ID
                    where ca.Username == customerAccountId
                    && c.ACSAplicationId == applicationId
                    select c;

                    var customerEntity = customerQuery.FirstOrDefault();

                    if (customerEntity != null)
                    {
                        // Get the telemetry session
                        var telemetryQuery =
                        from ts in dataWarehouseEntities.AndroWebTelemetrySessions
                        where ts.Id == new Guid(sessionId)
                        select ts;
                        
                        var telemetryEntity = telemetryQuery.FirstOrDefault();

                        if (telemetryEntity != null)
                        {
                            // Associate the telemetry session with the customer
                            telemetryEntity.CustomerId = customerEntity.ID;

                            // Commit the telemetry session
                            dataWarehouseEntities.SaveChanges();
                        }
                    }
                }
            }

            return "";
        }
    }
}
