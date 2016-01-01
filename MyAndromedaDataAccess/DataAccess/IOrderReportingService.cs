using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAndromedaDataAccess.Domain.Reporting.Query;
using MyAndromedaDataAccess.Domain.Reporting;

namespace MyAndromedaDataAccess.DataAccess
{
    public interface IOrderReportingService
    {
        OrdersSummary GetSummaryOfOrders(int siteId, FilterQuery filter);
    }
}
