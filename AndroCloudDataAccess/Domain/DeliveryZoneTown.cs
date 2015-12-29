using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AndroCloudDataAccess.Domain;
using System.Runtime.Serialization;

namespace AndroCloudDataAccess.Domain
{
    [XmlRoot(ElementName = "Town")] // Please don't change this
    public class DeliveryZoneTown
    {
        public string Name { get; set; }
        public string Postcodes { get; set; }
    }
}
