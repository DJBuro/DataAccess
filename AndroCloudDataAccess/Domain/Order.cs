using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AndroCloudDataAccess.Domain
{
    public class Order
    {
        [XmlElement("Guid")]
        public Guid OrderGuid { get; set; }
        public int Status { get; set; }
    }
}
