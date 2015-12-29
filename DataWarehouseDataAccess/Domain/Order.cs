using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DataWarehouseDataAccess.Domain
{
    public class Order
    {
        [JsonIgnore]
        [XmlIgnore]
        public Guid ID { get; set; }

        [JsonProperty(PropertyName = "status")]
        [XmlElement(ElementName = "Status")]
        public int RamesesStatusId { get; set; }

        [JsonProperty(PropertyName = "storeOrderId")]
        [XmlElement(ElementName = "storeOrderId")]
        public string StoreOrderId { get; set; }

        [JsonProperty(PropertyName = "isProvisional")]
        [XmlElement(ElementName = "IsProvisional")]
        public bool IsProvisional { get; set; }

        [JsonProperty(PropertyName = "driver")]
        [XmlElement(ElementName = "Driver")]
        public string Driver { get; set; }

        [JsonProperty(PropertyName = "driverId")]
        [XmlElement(ElementName = "DriverId")]
        public int? DriverId { get; set; }

        [JsonProperty(PropertyName = "ticketNumber")]
        [XmlElement(ElementName = "TicketNumber")]
        public int? TicketNumber { get; set; }

        public Order()
        {
            this.IsProvisional = false;
        }
    }
}
