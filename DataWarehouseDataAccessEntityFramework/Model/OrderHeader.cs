//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataWarehouseDataAccessEntityFramework.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderHeader
    {
        public OrderHeader()
        {
            this.OrderLines = new HashSet<OrderLine>();
            this.OrderLoyalties = new HashSet<OrderLoyalty>();
            this.UsedVouchers = new HashSet<UsedVoucher>();
        }
    
        public System.Guid ID { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.Guid> CustomerID { get; set; }
        public string OrderCurrency { get; set; }
        public string OrderType { get; set; }
        public Nullable<System.DateTime> OrderPlacedTime { get; set; }
        public Nullable<System.DateTime> OrderWantedTime { get; set; }
        public int ApplicationID { get; set; }
        public string ApplicationName { get; set; }
        public int RamesesOrderNum { get; set; }
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
        public int ACSErrorCode { get; set; }
        public string DestinationDevice { get; set; }
        public Nullable<System.Guid> CustomerAddressID { get; set; }
        public string ACSServer { get; set; }
        public string CookingInstructions { get; set; }
        public string DriverName { get; set; }
        public Nullable<int> DriverId { get; set; }
        public Nullable<int> TicketNumber { get; set; }
    
        public virtual ACSErrorCode ACSErrorCode1 { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual CustomerAddress CustomerAddress { get; set; }
        public virtual OrderStatu OrderStatu { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
        public virtual ICollection<OrderLoyalty> OrderLoyalties { get; set; }
        public virtual ICollection<UsedVoucher> UsedVouchers { get; set; }
    }
}
