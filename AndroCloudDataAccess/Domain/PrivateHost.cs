using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace AndroCloudDataAccess.Domain
{
    [XmlType("Host")]
    public class PrivateHost : IHost
    {
        [JsonIgnore]
        [XmlIgnore]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "order")]
        public int Order { get; set; }

        [JsonProperty(PropertyName = "signalRUrl")]
        public string SignalRUrl { get; set; }
    }
}
