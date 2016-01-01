using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroCloudDataAccess.Domain
{
    public class Site
    {
        public Guid SiteGuid { get; set; }
        public string Name { get; set; }
        public int MenuVersion { get; set; }
        public bool IsOpen { get; set; }
        public int EstDelivTime { get; set; }
    }
}
