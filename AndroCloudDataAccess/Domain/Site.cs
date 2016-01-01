using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace AndroCloudDataAccess.Domain
{
    [DataContract]
    public class Site
    {
        [DataMember(Name = "siteGuid")]
        [XmlElement("Guid")]
        public Guid SiteGuid { get; set; }

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
