using DataWarehouseDataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataWarehouseDataAccess.DataAccess
{
    public interface IOrderMetricsDataAccess
    {
        string ConnectionStringOverride { get; set; }
        string GetOrderMetrics(DateTime? fromDate, DateTime? toDate, int? applicationId, List<string> externalSiteId, out OrderMetrics orderMetrics);
        OrderMetrics GetOrderMetricsByACSOrder(Guid acsOrderId);
        IList<ACSErrorCode> GetAllACSErrorCodes();
        IList<OrderStatus> GetAllOrderStatus();
    }
}
