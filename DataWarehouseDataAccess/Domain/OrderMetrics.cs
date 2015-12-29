using DataWarehouseDataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataWarehouseDataAccess.Domain
{
    public class OrderMetrics
    {
        public List<SuccessfulOrder> SuccessfulOrders { get; set; }
        public List<FailedOrder> FailedOrders { get; set; }
    }
}
