using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataWarehouseDataAccess.DataAccess;
using DataWarehouseDataAccess.Domain;
using DataWarehouseDataAccessEntityFramework.Model;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DataWarehouseDataAccessEntityFramework.DataAccess
{
    public class OrderMetricsDataAccess : IOrderMetricsDataAccess
    {
        public string ConnectionStringOverride { get; set; }
        public string GetOrderMetrics(DateTime? fromDate, DateTime? toDate, int? applicationId, List<string> externalSiteIdList,
            out OrderMetrics orderMetrics)
        {
            orderMetrics = new OrderMetrics();

            orderMetrics.OrderList = new List<OrderHeaderDAO>();
            List<OrderHeaderDAO> tmpSuccssfulOrders = new List<OrderHeaderDAO>();
            using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
            {
                DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);
                var query = (
                            from oh in dataWarehouseEntities.OrderHeaders
                                .Include(e => e.OrderLines)
                                .Include(e => e.Customer)
                                .Include(e => e.CustomerAddress)
                                .Include(e => e.ACSErrorCode1)
                                .Where(x => (applicationId == null || x.ApplicationID == applicationId)
                                       && (externalSiteIdList.Count == 0 || externalSiteIdList.Any(s => s.ToLower().Trim().Equals(x.ExternalSiteID.ToLower())))
                                       && ((fromDate == null && toDate == null) || (x.OrderPlacedTime >= fromDate && x.OrderPlacedTime <= toDate)))
                            select oh);

                orderMetrics.OrderList = new List<OrderHeaderDAO>();
                foreach (DataWarehouseDataAccessEntityFramework.Model.OrderHeader oh in query)
                {
                    OrderHeaderDAO order = new OrderHeaderDAO()
                            {
                                ID = oh.ID,
                                TimeStamp = oh.TimeStamp,
                                CustomerID = oh.CustomerID,
                                OrderCurrency = oh.OrderCurrency,
                                OrderType = oh.OrderType,
                                OrderPlacedTime = oh.OrderPlacedTime,
                                OrderWantedTime = oh.OrderWantedTime,
                                ApplicationID = oh.ApplicationID,
                                ApplicationName = oh.ApplicationName,
                                RamesesOrderNum = oh.RamesesOrderNum,
                                ExternalOrderRef = oh.ExternalOrderRef,
                                ExternalSiteID = oh.ExternalSiteID,
                                SiteName = oh.SiteName,
                                ACSOrderId = oh.ACSOrderId,
                                paytype = oh.paytype,
                                FinalPrice = oh.FinalPrice,
                                TotalTax = oh.TotalTax,
                                DeliveryCharge = oh.DeliveryCharge,
                                PriceIncludeTax = oh.PriceIncludeTax,
                                PartnerName = oh.PartnerName,
                                Cancelled = oh.Cancelled,
                                Status = oh.Status,
                                ACSErrorCodeNumber = oh.ACSErrorCode,
                                
                                DestinationDevice = oh.DestinationDevice,
                                CustomerAddressID = oh.CustomerAddressID,
                                //Payload = aud.ExtraInfo,
                                //ACSServer = aud.ACSServer
                            };
                    if (oh.ACSErrorCode1 != null)
                    {
                        order.ACSErrorCode = new DataWarehouseDataAccess.Domain.ACSErrorCode()
                        {
                            ErrorCode = oh.ACSErrorCode1.ErrorCode,
                            ShortDescription = oh.ACSErrorCode1.ShortDescription,
                            LongDescription = oh.ACSErrorCode1.LongDescription
                        };
                    }
                    order.Customer = new DataWarehouseDataAccess.Domain.CustomerDAO()
                        {
                            ID = oh.Customer.ID,
                            Title = oh.Customer.Title,
                            FirstName = oh.Customer.FirstName,
                            LastName = oh.Customer.LastName,
                            AddressId = oh.Customer.AddressId,
                            ACSAplicationId = oh.Customer.ACSAplicationId,
                            RegisteredDateTime = oh.Customer.RegisteredDateTime,
                            CustomerAccountId = oh.Customer.CustomerAccountId
                        };
                    if (oh.CustomerAddress != null)
                    {
                        order.CustomerAddress = new DataWarehouseDataAccess.Domain.CustomerAddress()
                        {
                            ID = oh.CustomerAddress.ID,
                            CustomerKey = oh.CustomerAddress.CustomerKey,
                            RoadNum = oh.CustomerAddress.RoadNum,
                            RoadName = oh.CustomerAddress.RoadName,
                            City = oh.CustomerAddress.City,
                            State = oh.CustomerAddress.State,
                            ZipCode = oh.CustomerAddress.ZipCode,
                            Country = oh.CustomerAddress.Country
                        };
                    }
                    if (oh.OrderLines != null && oh.OrderLines.Count > 0)
                    {
                        order.OrderLines = new List<OrderLineDAO>();
                        foreach (DataWarehouseDataAccessEntityFramework.Model.OrderLine line in oh.OrderLines)
                        {
                            OrderLineDAO orderlineObj = new OrderLineDAO();
                            orderlineObj.ID = line.ID;
                            orderlineObj.OrderHeaderID = line.OrderHeaderID;
                            orderlineObj.ProductID = line.ProductID;
                            orderlineObj.Description = line.Description;
                            orderlineObj.Qty = line.Qty;
                            orderlineObj.Price = line.Price;
                            order.OrderLines.Add(orderlineObj);
                        }
                    }
                    orderMetrics.OrderList.Add(order);
                }
                prepareChartData(orderMetrics);
                #region old-query
                //using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
                //{
                //    DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);
                //    // Sample JSON: {"esid":"614A98C8-E7FF-48BF-95DF-CA6F8C1810AA","eoid":"516aa01741684ab78f275a18da7dd753","o":"{
                //    var query = (
                //                from aud in dataWarehouseEntities.Audits
                //                from oh in dataWarehouseEntities.OrderHeaders.
                //                Where(x => x.ExternalOrderRef.ToLower().Equals
                //                    (aud.ExtraInfo.Substring(aud.ExtraInfo.IndexOf("\"eoid\":\"") + 8,
                //                    (aud.ExtraInfo.IndexOf(",\"o\":") - (aud.ExtraInfo.IndexOf("\"eoid\":\"") + 9))).ToLower())
                //                           && (aud.ErrorCode == 0)
                //                           && (applicationId == null || x.ApplicationID == applicationId)
                //                           && (externalSiteIdList.Count == 0 || externalSiteIdList.Any(s => s.ToLower().Trim().Equals(x.ExternalSiteID.ToLower())))
                //                           && ((fromDate == null && toDate == null) || (x.OrderPlacedTime >= fromDate && x.OrderPlacedTime <= toDate)))
                //                select new DataWarehouseDataAccess.Domain.OrderHeaderDAO()
                //                {
                //                    ID = oh.ID,
                //                    TimeStamp = oh.TimeStamp,
                //                    CustomerID = oh.CustomerID,
                //                    OrderCurrency = oh.OrderCurrency,
                //                    OrderType = oh.OrderType,
                //                    OrderPlacedTime = oh.OrderPlacedTime,
                //                    OrderWantedTime = oh.OrderWantedTime,
                //                    ApplicationID = oh.ApplicationID,
                //                    ApplicationName = oh.ApplicationName,
                //                    RamesesOrderNum = oh.RamesesOrderNum,
                //                    ExternalOrderRef = oh.ExternalOrderRef,
                //                    ExternalSiteID = oh.ExternalSiteID,
                //                    SiteName = oh.SiteName,
                //                    ACSOrderId = oh.ACSOrderId,
                //                    paytype = oh.paytype,
                //                    FinalPrice = oh.FinalPrice,
                //                    TotalTax = oh.TotalTax,
                //                    DeliveryCharge = oh.DeliveryCharge,
                //                    PriceIncludeTax = oh.PriceIncludeTax,
                //                    PartnerName = oh.PartnerName,
                //                    Cancelled = oh.Cancelled,
                //                    Status = oh.Status,
                //                    Payload = aud.ExtraInfo,
                //                    ACSServer = aud.ACSServer
                //                }).ToList();
                //    orderMetrics.OrderList.AddRange(query);
            }
                #endregion
            return string.Empty;
        }

        private void prepareChartData(OrderMetrics orderMetrics)
        {
            IEnumerable<IGrouping<DateTime, OrderHeaderDAO>> GroupListByDate = orderMetrics.OrderList.OrderBy(o => o.OrderPlacedTime.Value.Date).GroupBy(o => o.OrderPlacedTime.Value.Date);
            List<DateTime> dateList = new List<DateTime>();
            List<string> dateSeries = new List<string>();
            List<double> successSeries = new List<double>();
            List<double> failedSeries = new List<double>();
            foreach (var group in GroupListByDate)
                dateList.Add(group.Key);

            foreach (DateTime dt in dateList)
            {
                dateSeries.Add(dt.ToString("dd-MMM-yyyy"));
                successSeries.Add(orderMetrics.OrderList.Where(o => o.ACSErrorCodeNumber == 0 && o.OrderPlacedTime.Value.Date.Equals(dt.Date)).Count());
                failedSeries.Add(orderMetrics.OrderList.Where(o => o.ACSErrorCodeNumber != 0 && o.OrderPlacedTime.Value.Date.Equals(dt.Date)).Count());
            }
            orderMetrics.DateSeries = dateSeries.ToArray();
            orderMetrics.SuccessSeries = successSeries.ToArray();
            orderMetrics.FailedSeries = failedSeries.ToArray();
        }

        public OrderMetrics GetOrderMetricsByACSOrder(Guid acsOrderId)
        {
            OrderMetrics orderMetrics = new OrderMetrics();

            orderMetrics.OrderList = new List<OrderHeaderDAO>();
            List<OrderHeaderDAO> tmpSuccssfulOrders = new List<OrderHeaderDAO>();
            using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
            {
                DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);
                var query = (
                            from oh in dataWarehouseEntities.OrderHeaders
                                .Include(e => e.OrderLines)
                                .Include(e => e.Customer)
                                .Include(e => e.CustomerAddress)
                                .Include(e => e.ACSErrorCode1)
                                .Where(x => x.ACSOrderId == acsOrderId)
                            select oh);

                orderMetrics.OrderList = new List<OrderHeaderDAO>();
                foreach (DataWarehouseDataAccessEntityFramework.Model.OrderHeader oh in query)
                {
                    OrderHeaderDAO order = new OrderHeaderDAO()
                            {
                                ID = oh.ID,
                                TimeStamp = oh.TimeStamp,
                                CustomerID = oh.CustomerID,
                                OrderCurrency = oh.OrderCurrency,
                                OrderType = oh.OrderType,
                                OrderPlacedTime = oh.OrderPlacedTime,
                                OrderWantedTime = oh.OrderWantedTime,
                                ApplicationID = oh.ApplicationID,
                                ApplicationName = oh.ApplicationName,
                                RamesesOrderNum = oh.RamesesOrderNum,
                                ExternalOrderRef = oh.ExternalOrderRef,
                                ExternalSiteID = oh.ExternalSiteID,
                                SiteName = oh.SiteName,
                                ACSOrderId = oh.ACSOrderId,
                                paytype = oh.paytype,
                                FinalPrice = oh.FinalPrice,
                                TotalTax = oh.TotalTax,
                                DeliveryCharge = oh.DeliveryCharge,
                                PriceIncludeTax = oh.PriceIncludeTax,
                                PartnerName = oh.PartnerName,
                                Cancelled = oh.Cancelled,
                                Status = oh.Status,
                                ACSErrorCodeNumber = oh.ACSErrorCode,

                                DestinationDevice = oh.DestinationDevice,
                                CustomerAddressID = oh.CustomerAddressID,
                                //Payload = aud.ExtraInfo,
                                //ACSServer = aud.ACSServer
                            };
                    if (oh.ACSErrorCode1 != null)
                    {
                        order.ACSErrorCode = new DataWarehouseDataAccess.Domain.ACSErrorCode()
                        {
                            ErrorCode = oh.ACSErrorCode1.ErrorCode,
                            ShortDescription = oh.ACSErrorCode1.ShortDescription,
                            LongDescription = oh.ACSErrorCode1.LongDescription
                        };
                    }
                    order.Customer = new DataWarehouseDataAccess.Domain.CustomerDAO()
                        {
                            ID = oh.Customer.ID,
                            Title = oh.Customer.Title,
                            FirstName = oh.Customer.FirstName,
                            LastName = oh.Customer.LastName,
                            AddressId = oh.Customer.AddressId,
                            ACSAplicationId = oh.Customer.ACSAplicationId,
                            RegisteredDateTime = oh.Customer.RegisteredDateTime,
                            CustomerAccountId = oh.Customer.CustomerAccountId
                        };
                    if (oh.CustomerAddress != null)
                    {
                        order.CustomerAddress = new DataWarehouseDataAccess.Domain.CustomerAddress()
                        {
                            ID = oh.CustomerAddress.ID,
                            CustomerKey = oh.CustomerAddress.CustomerKey,
                            RoadNum = oh.CustomerAddress.RoadNum,
                            RoadName = oh.CustomerAddress.RoadName,
                            City = oh.CustomerAddress.City,
                            State = oh.CustomerAddress.State,
                            ZipCode = oh.CustomerAddress.ZipCode,
                            Country = oh.CustomerAddress.Country
                        };
                    }
                    if (oh.OrderLines != null && oh.OrderLines.Count > 0)
                    {
                        order.OrderLines = new List<OrderLineDAO>();
                        foreach (DataWarehouseDataAccessEntityFramework.Model.OrderLine line in oh.OrderLines)
                        {
                            OrderLineDAO orderlineObj = new OrderLineDAO();
                            orderlineObj.ID = line.ID;
                            orderlineObj.OrderHeaderID = line.OrderHeaderID;
                            orderlineObj.ProductID = line.ProductID;
                            orderlineObj.Description = line.Description;
                            orderlineObj.Qty = line.Qty;
                            orderlineObj.Price = line.Price;
                            order.OrderLines.Add(orderlineObj);
                        }
                    }
                    orderMetrics.OrderList.Add(order);
                }
                return orderMetrics;
            }
        }
    }
}
