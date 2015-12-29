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
    public class OrderDataAccess : IOrderDataAccess
    {
        public string ConnectionStringOverride { get; set; }

        public string GetOrderHeadersByApplicationIdCustomerId(Guid? customerId, int applicationId, out List<DataWarehouseDataAccess.Domain.OrderHeader> orderHeaders)
        {
            orderHeaders = new List<DataWarehouseDataAccess.Domain.OrderHeader>();

            using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
            {
                DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);

                var query =
                    from oh in dataWarehouseEntities.OrderHeaders
                    join os in dataWarehouseEntities.OrderStatus
                        on oh.StatusId equals os.Id 
                    where oh.ApplicationID == applicationId
                    && oh.CustomerID == customerId
                    select new DataWarehouseDataAccess.Domain.OrderHeader()
                    {
                        Id = oh.ExternalOrderRef,
                        ForDateTime = oh.OrderWantedTime.Value,
                        Status = os.RamesesStatusId
                    };

                orderHeaders.AddRange(query);
            }

            return "";
        }

        public string GetOrderByOrderIdApplicationIdCustomerId(string externalOrderRef, Guid? customerId, int applicationId, out OrderDetails orderDetails)
        {
            orderDetails = null;

            using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
            {
                DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);

                var orderQuery =
                    from oh in dataWarehouseEntities.OrderHeaders
                    join os in dataWarehouseEntities.OrderStatus
                        on oh.StatusId equals os.Id
                    where oh.ApplicationID == applicationId
                    && oh.CustomerID == customerId
                    && oh.ExternalOrderRef == externalOrderRef
                    select new DataWarehouseDataAccess.Domain.OrderDetails()
                    {
                        Id = oh.ID,
                        ExternalOrderRef = oh.ExternalOrderRef,
                        ForDateTime = oh.OrderWantedTime.Value,
                        OrderStatus = os.RamesesStatusId,
                        OrderTotal = oh.FinalPrice
                    };

                OrderDetails orderDetailsEntity = orderQuery.FirstOrDefault();

                var orderLinesQuery =
                    from ol in dataWarehouseEntities.OrderLines
                    join oh in dataWarehouseEntities.OrderHeaders
                        on ol.OrderHeaderID equals oh.ID
                    where oh.ID == orderDetailsEntity.Id
                        && oh.ApplicationID == applicationId
                        && oh.CustomerID == customerId
                    select new DataWarehouseDataAccess.Domain.OrderLine()
                    {
                        MenuId = ol.ProductID.Value,
                        ProductName = ol.Description,
                        Quantity = ol.Qty.Value,
                        UnitPrice = ol.Price.Value,
                        ChefNotes = "",
                        Person = ""
                    };

                orderDetailsEntity.OrderLines = new List<DataWarehouseDataAccess.Domain.OrderLine>();
                orderDetailsEntity.OrderLines.AddRange(orderLinesQuery);

                orderDetails = orderDetailsEntity;
            }

            return "";
        }
    }
}
