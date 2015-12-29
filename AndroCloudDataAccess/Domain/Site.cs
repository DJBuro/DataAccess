using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace AndroCloudDataAccess.Domain
{
    [DataContract]
    public class Site
    {
        [JsonIgnore]
        [XmlIgnore]
        public Guid Id { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string LicenceKey { get; set; }

        [JsonProperty(PropertyName = "siteId")]
        [XmlElement("SiteId")]
        public string ExternalId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "menuVersion")]
        public int MenuVersion { get; set; }

        [JsonProperty(PropertyName = "isOpen")]
        public bool IsOpen { get; set; }

        [JsonProperty(PropertyName = "estDelivTime")]
        public int EstDelivTime { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public int AndroId { get; set; }
    }
}
