using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace AndroCloudDataAccess.Domain
{
    public class SiteDetails
    {
        [JsonIgnore]
        [XmlIgnore]
        public Guid Id { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string LicenceKey { get; set; }

        [JsonProperty(PropertyName = "siteId")]
        [XmlElement("SiteId", DataType = "string")]
        public string ExternalId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "menuVersion")]
        public int MenuVersion { get; set; }

        [JsonProperty(PropertyName = "isOpen")]
        public bool IsOpen { get; set; }

        [JsonProperty(PropertyName = "estDelivTime")]
        public int EstDelivTime { get; set; }

        [JsonProperty(PropertyName = "timeZone")]
        public string TimeZone { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "address", NullValueHandling=NullValueHandling.Ignore)]
        public Address Address { get; set; }

        [JsonProperty(PropertyName = "openingHours", NullValueHandling = NullValueHandling.Ignore)]
        public List<TimeSpanBlock> OpeningHours { get; set; }

        [JsonProperty(PropertyName = "paymentProvider")]
        public string PaymentProvider { get; set; }

        [JsonProperty(PropertyName = "paymentClientId")]
        public string PaymentClientId { get; set; }

        [JsonProperty(PropertyName = "paymentClientPassword")]
        public string PaymentClientPassword { get; set; }
    }
}
