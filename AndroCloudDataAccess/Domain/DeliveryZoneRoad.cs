using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AndroCloudDataAccess.Domain;
using System.Runtime.Serialization;

namespace AndroCloudDataAccess.Domain
{
    [XmlRoot(ElementName = "Road")] // Please don't change this
    public class DeliveryZoneRoad
    {
        public string Postcode { get; set; }
        public string RoadName { get; set; }
        public bool DeliversToWholeRoad { get; set; }
        [XmlElement("HouseNumberStart", IsNullable = false)]  // <HouseNumberStart p3:nil="true" xmlns:p3="http://www.w3.org/2001/XMLSchema-instance" />
        public string HouseNumberStart { get; set; }
        [XmlElement("HouseNumberEnd", IsNullable = false)]
        public string HouseNumberEnd { get; set; }
        public int RuleId { get; set; }
        public string ExternalSiteId { get; set; }
    }
}
