using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataWarehouseDataAccess.DataAccess;
using DataWarehouseDataAccess.Domain;
using DataWarehouseDataAccessEntityFramework.Model;

namespace DataWarehouseDataAccessEntityFramework.DataAccess
{
    public class OrderMetricsDataAccess : IOrderMetricsDataAccess
    {
        public string ConnectionStringOverride { get; set; }
        public string GetOrderMetrics(DateTime? fromDate, DateTime? toDate, int? applicationId, List<string> externalSiteIdList, 
            out OrderMetrics orderMetrics)
        {
            orderMetrics = new OrderMetrics();

            orderMetrics.SuccessfulOrders = new List<SuccessfulOrder>();
            List<SuccessfulOrder> tmpSuccssfulOrders = new List<SuccessfulOrder>();
            orderMetrics.FailedOrders = new List<FailedOrder>();

            using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
            {
                DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);
                // Sample JSON: {"esid":"614A98C8-E7FF-48BF-95DF-CA6F8C1810AA","eoid":"516aa01741684ab78f275a18da7dd753","o":"{
                var query = (
                            from aud in dataWarehouseEntities.Audits
                            from oh in dataWarehouseEntities.OrderHeaders.
                            Where(x => x.ExternalOrderRef.ToLower().Equals
                                (aud.ExtraInfo.Substring(aud.ExtraInfo.IndexOf("\"eoid\":\"") + 8,
                                (aud.ExtraInfo.IndexOf(",\"o\":") - (aud.ExtraInfo.IndexOf("\"eoid\":\"") + 9))).ToLower())
                                       && (aud.ErrorCode == 0)
                                       && (applicationId == null || x.ApplicationID == applicationId)
                                       && (externalSiteIdList.Count == 0 || externalSiteIdList.Any(s => s.ToLower().Trim().Equals(x.ExternalSiteID.ToLower())))
                                       && ((fromDate == null && toDate == null) || (x.OrderPlacedTime >= fromDate && x.OrderPlacedTime <= toDate)))
                            select new DataWarehouseDataAccess.Domain.SuccessfulOrder()
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
                                Payload = aud.ExtraInfo,
                                ACSServer = aud.ACSServer
                            }).ToList();
                orderMetrics.SuccessfulOrders.AddRange(query);
            }
            return string.Empty;
        }
    }
}
