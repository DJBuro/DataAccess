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
                    where oh.ApplicationID == applicationId
                    && oh.CustomerID == customerId
                    orderby oh.OrderPlacedTime descending
                    select new DataWarehouseDataAccess.Domain.OrderHeader()
                    {
                        Id = oh.ExternalOrderRef, // oh.RamesesOrderNum,
                        ForDateTime = oh.OrderWantedTime.Value,
                        Status = oh.Status,
                        Driver = oh.DriverName
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
                    where oh.ApplicationID == applicationId
                        && oh.CustomerID == customerId
                        && oh.ExternalOrderRef == externalOrderRef
                    orderby oh.OrderPlacedTime descending
                    select new DataWarehouseDataAccess.Domain.OrderDetails()
                    {
                        Id = oh.ID,
                        ExternalOrderRef = oh.ExternalOrderRef,
                        ForDateTime = oh.OrderWantedTime.Value,
                        OrderStatus = oh.Status,
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
                    select ol;

                var orderLines = orderLinesQuery.ToList();

                // Prepare the results
                orderDetails = new OrderDetails()
                {
                    ExternalOrderRef = orderDetailsEntity.ExternalOrderRef,
                    ForDateTime = orderDetailsEntity.ForDateTime,
                    OrderStatus = orderDetailsEntity.OrderStatus,
                    OrderTotal = orderDetailsEntity.OrderTotal,
                    OrderLines = new List<DataWarehouseDataAccess.Domain.OrderLine>(),
                    Deals = new List<DataWarehouseDataAccess.Domain.OrderLine>()
                };

                Dictionary<Guid, DataWarehouseDataAccess.Domain.OrderLine> dealsLookup = new Dictionary<Guid, DataWarehouseDataAccess.Domain.OrderLine>();

                foreach (var orderLineEntity in orderLinesQuery)
                {
                    // Order line
                    DataWarehouseDataAccess.Domain.OrderLine orderLine = new DataWarehouseDataAccess.Domain.OrderLine()
                    {
                        MenuId = orderLineEntity.ProductID.Value,
                        ProductName = orderLineEntity.Description,
                        Quantity = orderLineEntity.Qty.Value,
                        UnitPrice = orderLineEntity.Price.Value,
                        ChefNotes = "",
                        Person = "",
                        Modifiers = new List<Modifier>()
                    };

                    // Add toppings
                    foreach (modifier modifierEntity in orderLineEntity.modifiers)
                    {
                        Modifier modifier = new Modifier()
                        {
                            Description = modifierEntity.Description,
                            Price = modifierEntity.Price,
                            Quantity = modifierEntity.Qty,
                            Removed = modifierEntity.Removed
                        };

                        orderLine.Modifiers.Add(modifier);
                    }

                    if ((orderLineEntity.IsDeal.HasValue && orderLineEntity.IsDeal.Value) ||
                        orderLineEntity.DealID.HasValue)
                    {
                        // It's a deal or deal line
                        if (orderLineEntity.IsDeal.HasValue && orderLineEntity.IsDeal.Value)
                        {
                            // Is there already a placeholder deal?
                            DataWarehouseDataAccess.Domain.OrderLine dealOrderLine = null;
                            if (dealsLookup.TryGetValue(orderLineEntity.ID, out dealOrderLine))
                            {
                                // Copy the child deal lines over from the placeholder deal and then remove the placeholder
                                orderLine.ChildOrderLines = dealOrderLine.ChildOrderLines;
                                dealsLookup.Remove(orderLineEntity.ID);
                            }

                            if (orderLine.ChildOrderLines == null) orderLine.ChildOrderLines = new List<DataWarehouseDataAccess.Domain.OrderLine>();

                            // It's a deal - add it to the lookup
                            dealsLookup.Add(orderLineEntity.ID, orderLine);

                            // Add the deal to the order
                            orderDetails.Deals.Add(orderLine);
                        }
                        else
                        {
                            // It's a deal line
                            DataWarehouseDataAccess.Domain.OrderLine dealOrderLine = null;
                            if (!dealsLookup.TryGetValue(orderLineEntity.DealID.Value, out dealOrderLine))
                            {
                                // No deal for this deal line
                                // Because the order of the order lines can be random we might have got a deal line before the deal itself
                                // Add a place holder for the deal which we can replace later
                                dealOrderLine = new DataWarehouseDataAccess.Domain.OrderLine();
                                dealOrderLine.ChildOrderLines = new List<DataWarehouseDataAccess.Domain.OrderLine>();
                                dealsLookup.Add(orderLineEntity.DealID.Value, dealOrderLine);
                            }

                            // Add the deal line to the deal
                            dealOrderLine.ChildOrderLines.Add(orderLine);
                        }
                    }
                    else
                    {
                        // It's a normal order line
                        orderDetails.OrderLines.Add(orderLine);
                    }
                }
            }

            return "";
        }

        public string UpdateOrderStatus
        (
            int ramesesOrderNumber, 
            string externalSiteID, 
            int ramesesOrderStatusId, 
            string driverName,
            int? driverId,
            int? ticketNumber)
        {
            using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
            {
                DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);

                var query =
                    from oh in dataWarehouseEntities.OrderHeaders
                    where oh.ExternalSiteID == externalSiteID
                    && oh.RamesesOrderNum == ramesesOrderNumber
                    select oh;

                var orderHeaderEntity = query.FirstOrDefault();

                if (orderHeaderEntity == null)
                {
                    return "Unknown orderId '" + ramesesOrderNumber + "' and site id '" + externalSiteID + "' combination";
                }
                
                // Update the order status
                orderHeaderEntity.Status = ramesesOrderStatusId;

                if (!String.IsNullOrEmpty(driverName))
                {
                    orderHeaderEntity.DriverName = driverName;
                    orderHeaderEntity.DriverId = driverId;
                    orderHeaderEntity.TicketNumber = ticketNumber;
                }

                // Update the order status history
                dataWarehouseEntities.OrderStatusHistories.Add
                (
                    new OrderStatusHistory() 
                    {
                        Id = Guid.NewGuid(),
                        OrderHeaderId = orderHeaderEntity.ID,
                        Status = ramesesOrderStatusId,
                        ChangedDateTime = DateTime.UtcNow,
                    }
                );

                dataWarehouseEntities.SaveChanges();
            }

            return "";
        }

        public string GetByExternalIdApplicationId(string externalOrderId, int applicationId, out DataWarehouseDataAccess.Domain.Order order)
        {
            order = null;

            using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
            {
                DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);

                var acsQuery = from o in dataWarehouseEntities.OrderHeaders
                               where o.ExternalOrderRef == externalOrderId
                                    && o.ApplicationID == applicationId
                               select o;

                var acsQueryEntity = acsQuery.FirstOrDefault();

                if (acsQueryEntity != null)
                {
                    order = new DataWarehouseDataAccess.Domain.Order();
                    order.ID = acsQueryEntity.ID;
                    order.StoreOrderId = acsQueryEntity.RamesesOrderNum.ToString();
                    order.RamesesStatusId = acsQueryEntity.OrderStatu.Id;
                    order.Driver = acsQueryEntity.DriverName;
                    order.DriverId = acsQueryEntity.DriverId;
                    order.TicketNumber = acsQueryEntity.TicketNumber;
                }
            }

            return "";
        }
    }
}
