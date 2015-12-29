using System;
using System.Xml.Serialization;

namespace AndroCloudDataAccess.Domain
{
    public class SiteMenu
    {
        [XmlElement("Guid")]
        public Guid SiteID { get; set; }
        public string MenuType { get; set; }
        public int Version { get; set; }
        public string MenuData { get; set; }
    }
}
