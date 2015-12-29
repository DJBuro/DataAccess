using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace AndroCloudDataAccess.Domain
{
    public class Site
    {
        [JsonIgnore]
        [NonSerialized]
        public Guid Id { get; set; }

        [JsonIgnore]
        [NonSerialized]
        public string LicenceKey { get; set; }

        [JsonProperty(PropertyName = "siteGuid")]
        public string ExternalId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "menuVersion")]
        public int MenuVersion { get; set; }

        [JsonProperty(PropertyName = "isOpen")]
        public bool IsOpen { get; set; }

        [JsonProperty(PropertyName = "estDelivTime")]
        public int EstDelivTime { get; set; }
    }
}
