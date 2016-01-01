using System;
using System.Collections.Generic;
using System.Linq;

namespace MyAndromedaDataAccess.Domain
{
    public class SiteDetails
    {
        public int Id { get; set; }
        public string LicenceKey { get; set; }
        public int AndroSiteId { get; set; }
        public string CustomerSiteId { get; set; }
        public string ExternalSiteId { get; set; }
        public string AndroSiteName { get; set; }
        public string ClientSiteName { get; set; }
        public string ExternalSiteName { get; set; }
        public int MenuVersion { get; set; }
        public bool IsOpen { get; set; }
        public int EstDelivTime { get; set; }
        public string TimeZone { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        //public List<TimeSpanBlock> OpeningHours { get; set; }
        public string PaymentProvider { get; set; }
        public string PaymentClientId { get; set; }
        public string PaymentClientPassword { get; set; }
    }
}
