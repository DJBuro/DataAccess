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
    public class TelemetrySession
    {
        [JsonIgnore]
        [XmlIgnore]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "createdDateTime")]
        [XmlElement(ElementName = "CreatedDateTime")]
        public string CreatedDateTime { get; set; }

        [JsonProperty(PropertyName = "customerId")]
        [XmlElement(ElementName = "CustomerId")]
        public string CustomerId { get; set; }

        [JsonProperty(PropertyName = "referrer")]
        [XmlElement(ElementName = "Referrer")]
        public string Referrer { get; set; }

        [JsonProperty(PropertyName = "browserDetails")]
        [XmlElement(ElementName = "BrowserDetails")]
        public string BrowserDetails { get; set; }
    }
}
