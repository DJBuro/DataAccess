using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardDataAccess.Domain
{
    public class Region
    {
        public virtual int Id { get; set; }
        public virtual string RegionName { get; set; }
        public virtual int HeadOfficeID { get; set; }
        public virtual string TimeZone { get; set; }
    }
}