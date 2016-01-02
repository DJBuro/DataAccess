using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataWarehouseDataAccess.Domain;

namespace DataWarehouseDataAccess.DataAccess
{
    public interface ITelemetryDataAccess
    {
        string ConnectionStringOverride { get; set; }

        string AddTelemetrySession
        (
            int applicationId,
            DataWarehouseDataAccess.Domain.TelemetrySession telemetrySession,
            string externalSiteId
        );

        string UpdateTelemetrySession
        (
            int applicationId,
            string customerAccountId,
            string sessionId
        );

        string AddTelemetry
        (
            int applicationId,
            DataWarehouseDataAccess.Domain.Telemetry telemetry
        );
    }
}
