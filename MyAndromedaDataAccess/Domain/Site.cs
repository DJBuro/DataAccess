using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MyAndromedaDataAccess.Domain
{
    public class Site
    {
        public int Id { get; set; }
        public int AndromediaSiteId { get; set; }
        public string LicenceKey { get; set; }
        public string CustomerSiteId { get; set; }
        public string ClientSiteName { get; set; }
        public string ExternalName { get; set; }
        public int MenuVersion { get; set; }
        public bool IsOpen { get; set; }
        public int EstDelivTime { get; set; }
        public string ExternalSiteId { get; set; }

        public int ChainId { get; set; }
    }
}
