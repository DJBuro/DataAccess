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
    public class Host
    {
        [JsonIgnore]
        [XmlIgnore]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "hostName")]
        public string HostName { get; set; }

        [JsonProperty(PropertyName = "order")]
        public int Order { get; set; }

        [JsonProperty(PropertyName = "port")]
        public int Port { get; set; }
    }
}
