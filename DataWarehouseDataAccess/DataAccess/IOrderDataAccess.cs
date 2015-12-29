using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataWarehouseDataAccess.Domain;

namespace DataWarehouseDataAccess.DataAccess
{
    public interface IOrderDataAccess
    {
        string ConnectionStringOverride { get; set; }

        string GetOrderHeadersByApplicationIdCustomerId(Guid? customerId, int applicationId, out List<DataWarehouseDataAccess.Domain.OrderHeader> orderHeaders);
        string GetOrderByOrderIdApplicationIdCustomerId(string externalOrderRef, Guid? customerId, int applicationId, out OrderDetails orderDetails);
        string UpdateOrderStatus(string internetOrderNumber, string externalSiteID, int ramesesOrderStatusId);
        string GetByExternalIdApplicationId(string externalOrderId, int applicationId, out DataWarehouseDataAccess.Domain.Order order);
    }
}
