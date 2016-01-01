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

        [JsonIgnore]
        [XmlIgnore]
        public int InternetOrderNumber { get; set; }

        [JsonProperty(PropertyName = "storeOrderId")]
        [XmlElement(ElementName = "storeOrderId")]
        public string StoreOrderId { get; set; }
    }
}
