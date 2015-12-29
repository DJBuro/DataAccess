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
        public string MenuDataThumbnails { get; set; }
        public string MenuDataExtended { get; set; }
        public string MenuDataExtendedVersion { get; set; }
    }
}
