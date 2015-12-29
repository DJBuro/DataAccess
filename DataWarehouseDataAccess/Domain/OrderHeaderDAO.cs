using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataWarehouseDataAccess.Domain
{
    /// <summary>
    /// Data from OrderHeader table in Datawarehouse schema
    /// </summary>
    public class OrderHeaderDAO
    {
        public System.Guid ID { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.Guid> CustomerID { get; set; }
        public string OrderCurrency { get; set; }
        public string OrderType { get; set; }
        public Nullable<System.DateTime> OrderPlacedTime { get; set; }
        public Nullable<System.DateTime> OrderWantedTime { get; set; }
        public int ApplicationID { get; set; }
        public string ApplicationName { get; set; }
        public string RamesesOrderNum { get; set; }
        public string ExternalOrderRef { get; set; }
        public string ExternalSiteID { get; set; }
        public string SiteName { get; set; }
        public System.Guid ACSOrderId { get; set; }
        public string paytype { get; set; }
        public decimal FinalPrice { get; set; }
        public decimal TotalTax { get; set; }
        public decimal DeliveryCharge { get; set; }
        public bool PriceIncludeTax { get; set; }
        public string PartnerName { get; set; }
        public bool Cancelled { get; set; }
        public int Status { get; set; }
        public int ACSErrorCodeNumber { get; set; }
        public int IsFailed { get { return (ACSErrorCodeNumber != 0 && Status != 6) ? 1 : 0; } }
        public int IsSuccess { get { return (ACSErrorCodeNumber == 0 && Status != 6) ? 1 : 0; } }
        public int IsCancelled { get { return Status == 6 ? 1 : 0; } }
        public string DestinationDevice { get; set; }
        public Nullable<System.Guid> CustomerAddressID { get; set; }
        public string ACSServer { get; set; }

        public string Payload { get; set; }

        public CustomerDAO Customer { get; set; }
        public CustomerAddress CustomerAddress { get; set; }
        public ACSErrorCode ACSErrorCode { get; set; }
        public ICollection<OrderLineDAO> OrderLines { get; set; }
        public ICollection<UsedVoucher> UsedVouchers { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
