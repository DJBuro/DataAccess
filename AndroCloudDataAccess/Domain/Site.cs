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

        [DataMember(Name = "siteGuid")]
        public string ExternalId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "menuVersion")]
        public int MenuVersion { get; set; }

        [DataMember(Name = "isOpen")]
        public bool IsOpen { get; set; }

        [DataMember(Name = "estDelivTime")]
        public int EstDelivTime { get; set; }
    }
}
