using System;
using System.Linq;
using MyAndromeda.Core.Site;

namespace MyAndromedaDataAccess.Domain
{
    public class Site : ISite
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

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public int ChainId { get; set; }
    }
}
