using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace AndroCloudDataAccess.Domain
{
    public class Order
    {
        [JsonIgnore]
        [XmlIgnore]
        public Guid ID { get; set;}

        [JsonProperty(PropertyName = "status")]
        [XmlElement(ElementName = "Status")]
        public int RamesesStatusId { get; set;}

        [JsonIgnore]
        [XmlIgnore]
        public int InternetOrderNumber { get; set;}

        [JsonProperty(PropertyName = "orderId")]
        [XmlElement(ElementName="OrderId")]
        public string ExternalID { get; set;}
    }
}
