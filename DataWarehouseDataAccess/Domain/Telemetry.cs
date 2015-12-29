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
    public class Telemetry
    {
        [JsonProperty(PropertyName = "sessionId")]
        [XmlElement(ElementName = "SessionId")]
        public string SessionId { get; set; }

        [JsonProperty(PropertyName = "action")]
        [XmlElement(ElementName = "Action")]
        public string Action { get; set; }

        [JsonProperty(PropertyName = "dateTime")]
        [XmlElement(ElementName = "DateTime")]
        public string DateTime { get; set; }
    }
}
